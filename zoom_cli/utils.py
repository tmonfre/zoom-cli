import os
import json
import subprocess

__version__ = "1.1.5"

ZOOM_CLI_DIR = os.path.expanduser("~/.zoom-cli")
SAVE_FILE_PATH = "{}/meetings.json".format(ZOOM_CLI_DIR)

# adopted from: https://stackoverflow.com/questions/8924173/how-do-i-print-bold-text-in-python
class ConsoleColor:
   PURPLE = '\033[95m'
   CYAN = '\033[96m'
   DARKCYAN = '\033[36m'
   BLUE = '\033[94m'
   GREEN = '\033[92m'
   YELLOW = '\033[93m'
   RED = '\033[91m'
   BOLD = '\033[1m'
   UNDERLINE = '\033[4m'
   END = '\033[0m'

def dict_to_json_string(dict):
    def dumper(obj):
        try:
            return obj.toJSON()
        except:
            return obj.__dict__

    return json.dumps(dict, default=dumper, indent=2)

def get_meeting_file_contents():
    try:
        with open(SAVE_FILE_PATH, "r") as file:
            return json.loads(file.read())
    except:
        return {}

def get_meeting_names():
    return sorted(get_meeting_file_contents().keys())

def write_to_meeting_file(contents):
    with open(SAVE_FILE_PATH, "w") as file:
        file.write(dict_to_json_string(contents))

def is_command_available(command):
    s = subprocess.Popen("command -v {}".format(command), shell=True, stdout=subprocess.PIPE)
    output = s.stdout.read().decode("utf-8")
    return len(output) > 0

def launch_zoommtg_url(url, password=""):
    url_to_launch = url + "?pwd={}".format(password) if password else url
    command = "open" if is_command_available("open") else "xdg-open"
    os.system('{} "{}"'.format(command, url_to_launch))

def launch_zoommtg(id, password):
    url = "zoommtg://zoom.us/join?confno=" + id
    launch_zoommtg_url(url, password)
