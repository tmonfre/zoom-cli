import os
from zoom_cli.utils import ZOOM_CLI_DIR, SAVE_FILE_PATH, __version__

# create zoom-cli directory 
if not os.path.isdir(ZOOM_CLI_DIR):
    os.makedirs(ZOOM_CLI_DIR)

# create meetings.json file
if not os.path.exists(SAVE_FILE_PATH):
    with open(SAVE_FILE_PATH,'w+') as file:
        file.write("{}")