import os
from PyInquirer import prompt

from zoom_cli.utils import ConsoleColor, get_meeting_file_contents, write_to_meeting_file, launch_zoommtg, launch_zoommtg_url

def _launch_url(url):
    try:
        url_to_launch = url[url.index("://")+3:] if "://" in url else url
        launch_zoommtg_url("zoommtg://{}".format(url_to_launch))

    except:
        print(ConsoleColor.BOLD + "Error:" + ConsoleColor.END, end=' ')
        print("Unable to launch given URL:  " + ConsoleColor.BOLD + url + ConsoleColor.END + ".")

def _launch_name(name):
    contents = get_meeting_file_contents()

    if name in contents:
        if "url" in contents[name]:
            url = contents[name]["url"]
            
            # grab full id from url, go to end of string or until hit query params
            id = url[url.index("/j/")+3:min(len(url), url.index("?") if "?" in url else float("inf"))]
            password = ""

            # grab password from url if provided
            if "pwd=" in url:
                password = url[url.index("pwd=")+4:min(len(url), url.index("&") if "&" in url else float("inf"))]
            
            launch_zoommtg(id, contents[name]["password"] if "password" in contents[name] else password)
        elif "id" in contents[name]:
            launch_zoommtg(
                contents[name]["id"], 
                contents[name]["password"] if "password" in contents[name] else ""
            )
        else:
            print(ConsoleColor.BOLD + "Error:" + ConsoleColor.END, end=' ')
            print("No url or id found for meeting with title " + ConsoleColor.BOLD + name + ConsoleColor.END + ".")
    else:
        print(ConsoleColor.BOLD + "Error:" + ConsoleColor.END, end=' ')
        print("Could not find meeting with title " + ConsoleColor.BOLD + name + ConsoleColor.END + ".")

def _save_url(name, url, password):
    contents = get_meeting_file_contents()
    contents[name] = { "url": url }
    if password: contents[name]["password"] = password
    write_to_meeting_file(contents)

def _save_id_password(name, id, password):
    contents = get_meeting_file_contents()
    contents[name] = { "id": id }
    if password: contents[name]["password"] = password
    write_to_meeting_file(contents)

def _edit(name, url, id, password):
    contents = get_meeting_file_contents()
    new_dict = {}

    if url: new_dict["url"] = url
    if id: new_dict["id"] = id
    if password: new_dict["password"] = password

    for key, val in contents[name].items():
        new_dict[key] = prompt({
            'type': 'input',
            'name': key,
            'message': key,
            'default': new_dict[key] if key in new_dict else val
        })[key]

    del contents[name]
    contents[name] = new_dict
    write_to_meeting_file(contents)

def _remove(name):
    contents = get_meeting_file_contents()
    del contents[name]
    write_to_meeting_file(contents)

def _ls():
    meetings = get_meeting_file_contents()
    idx = 0

    for (name, entries) in meetings.items():
        print(ConsoleColor.BOLD + name + ConsoleColor.END)
        if "url" in entries: print(ConsoleColor.BOLD + "    url: " + ConsoleColor.END + entries["url"])
        if "id" in entries: print(ConsoleColor.BOLD + "    id: " + ConsoleColor.END + entries["id"])
        if "password" in entries: print(ConsoleColor.BOLD + "    password: " + ConsoleColor.END + entries["password"])

        if idx < len(meetings) - 1: print()
        idx += 1
