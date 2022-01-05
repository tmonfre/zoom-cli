import os
import json

ZOOM_CLI_DIR = os.path.expanduser("~/.zoom-cli-new")
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

def write_to_meeting_file(contents):
    with open(SAVE_FILE_PATH, "w") as file:
        file.write(dict_to_json_string(contents))
