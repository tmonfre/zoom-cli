import os
import click
from click_default_group import DefaultGroup
from PyInquirer import prompt

from commands import _edit, _launch_url, _launch_name, _ls, _remove, _save_url, _save_id_password
from utils import ZOOM_CLI_DIR, SAVE_FILE_PATH, get_meeting_names

@click.group(cls=DefaultGroup, default='launch', default_if_no_args=True)
def main():
    if not os.path.isdir(ZOOM_CLI_DIR):
        print("making directory")
        os.makedirs(ZOOM_CLI_DIR)

    if not os.path.exists(SAVE_FILE_PATH):
        print("writing to file")
        with open(SAVE_FILE_PATH,'w+') as file:
            file.write("{}")

@main.command(help="Launch meeting [url or saved meeting name]")
@click.argument('url_or_name')
def launch(url_or_name):
    if "https://" in url_or_name and "zoom.us" in url_or_name:
        _launch_url(url_or_name)
    else:
        _launch_name(url_or_name)

@main.command(help="Save meeting")
@click.option("--name", '-n', default="", help="Meeting name")
@click.option("--url", default="", help="Zoom URL (must provide this or meeting ID/password)")
@click.option("--id", default="", help="Zoom meeting ID")
@click.option("--password", '-p', default="", help="Zoom password")
def save(name, url, id, password):
    # prompt name if not provided
    if not name:
        name = prompt({
            'type': 'input',
            'name': 'name',
            'message': 'Meeting name:',
        })["name"]

    save_as_url = None

    # determine storage method if not indicated
    if not url and not id:
        save_as_url = prompt({
            'type': 'list',
            'name': "url_or_id",
            'message': 'Store as URL or Meeting ID/Password?',
            'choices': ['URL', 'Meeting ID/Password']
        })["url_or_id"] == "URL"

    # get url if not provided and saving that way
    if not url and save_as_url == True:
        url = prompt({
            'type': 'input',
            'name': 'url',
            'message': 'Zoom URL:',
        })["url"]

    # get meeting id and password if saving that way
    if not id and save_as_url == False:
        id_password_choices = prompt([{
            'type': 'input',
            'name': 'id',
            'message': 'Meeting ID:',
        }, {
            'type': 'input',
            'name': 'password',
            'message': 'Meeting password:',
        }])

        id = id_password_choices["id"]
        password = id_password_choices["password"]

    if name and url:
        _save_url(name, url)
    elif name and id:
        _save_id_password(name, id, password)

@main.command(help="Edit meeting")
@click.option("--name", '-n', default="", help="Meeting name")
@click.option("--url", default="", help="Zoom URL (must provide this or meeting ID/password)")
@click.option("--id", default="", help="Zoom meeting ID")
@click.option("--password", '-p', default="", help="Zoom password")
def edit(name, url, id, password):
    # prompt name if not provided
    if not name:
        name = prompt({
            'type': 'list',
            'name': 'name',
            'message': 'Meeting name:',
            'choices': get_meeting_names()
        })["name"]

    _edit(name, url, id, password)

@main.command(help="Delete meeting")
@click.argument('name')
def rm(name):
    _remove(name)

@main.command(help="List all saved meetings")
def ls():
    _ls()
    
if __name__ == '__main__':
    main()


#############################
##  zoom [url]
##  zoom [name]
##  zoom save -n [name] --url [url]
##  zoom save -n [name] --id [id] -p [password]
##  zoom ls
##  zoom rm [name]
##  zoom edit [name] (can provide options for url, id, and password. Will prompt for everything missing)
#############################