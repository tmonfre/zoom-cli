import os
from utils import ConsoleColor, get_meeting_file_contents, write_to_meeting_file, launch_zoommtg

def _launch_url(url):
    try:
        url_string = url[url.index("/j/") + 3:]
        id = url_string
        password = ""

        if "pwd=" in url_string:
            id = url_string[:url_string.index("pwd=") - 1]
            password = url_string[url_string.index("pwd=") + 4:]

        launch_zoommtg(id, password)
        
    except:
        print(ConsoleColor.BOLD + "Error:" + ConsoleColor.END, end=' ')
        print("Unable to launch given URL:  " + ConsoleColor.BOLD + url + ConsoleColor.END + ".")

def _launch_name(name):
    contents = get_meeting_file_contents()

    if name in contents:
        launch_zoommtg(
            contents[name]["id"], 
            contents[name]["password"] if "password" in contents[name] else ""
        )
    else:
        print(ConsoleColor.BOLD + "Error:" + ConsoleColor.END, end=' ')
        print("Could not find meeting with title " + ConsoleColor.BOLD + name + ConsoleColor.END + ".")

def _save_url(name, url):
    contents = get_meeting_file_contents()
    contents[name] = { "url": url }
    write_to_meeting_file(contents)

def _save_id_password(name, id, password):
    contents = get_meeting_file_contents()
    contents[name] = { "id": id }
    if password: contents[name]["password"] = password
    write_to_meeting_file(contents)

def _edit(name, url, id, password):
    print("EDIT | name: {}, url: {}, id: {}, password: {}".format(name, url, id, password))

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
