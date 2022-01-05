from utils import ConsoleColor, get_meeting_file_contents

def _launch_url(url):
    print("LAUNCH URL | url: {}".format(url))

def _launch_name(name):
    print("LAUNCH NAME | name: {}".format(name))

def _save_url(name, url):
    print("SAVE URL | name: {}, url: {}".format(name, url))

def _save_id_password(name, id, password):
    print("SAVE ID/PASSWORD | name: {}, id: {}, password: {}".format(name, id, password))

def _edit(name, url, id, password):
    print("EDIT | name: {}, url: {}, id: {}, password: {}".format(name, url, id, password))

def _remove(name):
    print("REMOVE | name: {}".format(name))

def _ls():
    meetings = get_meeting_file_contents()

    for (name, entries) in meetings.items():
        print(ConsoleColor.BOLD + name + ConsoleColor.END)
        if "url" in entries: print(ConsoleColor.BOLD + "    url: " + ConsoleColor.END + entries["url"])
        if "id" in entries: print(ConsoleColor.BOLD + "    id: " + ConsoleColor.END + entries["id"])
        if "password" in entries: print(ConsoleColor.BOLD + "    password: " + ConsoleColor.END + entries["password"])
        print()