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
    print("LIST")