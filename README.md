# Zoom CLI

`zoom` is a command line tool that allows users to store, access, and launch Zoom meetings on the fly.

It is written in Python and available to install via Homebrew.

## Installation Instructions

### Mac/Linux Users

1. Download and install Homebrew: [https://brew.sh](https://brew.sh).
2. `brew tap tmonfre/homebrew-tmonfre`
3. `brew install zoom`
4. (Only if zoom cask is also installed) `brew link zoom`

### PC Users

This package is currently not yet available on Scoop. Please follow the [developer instructions](#developer-instructions) below in the meantime.

## Usage

Below are the available commands. If an option/flag listed below is ommitted, you will be prompted to enter its value.

### Launch Meetings

- `zoom [url]` to launch any meeting on the fly.
- `zoom [name]` to launch a saved meeting by name.

### Save Meetings

- `zoom save` to save a new meeting
  - `-n, --name` meeting name
  - `--id` meeting ID
  - `--password` meeting password (optional)
  - `--url` meeting URL (optional, must  provide this or `--id`)

- `zoom edit` to edit a stored meeting
  - `-n, --name` meeting name (optional)
  - `--id` meeting ID (optional)
  - `--password` meeting password (optional)
  - `--url` meeting URL (optional)

- `zoom rm [name]` to delete a stored meeting

- `zoom ls` to see all stored meetings

## Developer Instructions

Interested in contributing? Follow the steps below to install the project locally. Feel free to address any open issues, bug fixes, or feature requests by opening a pull request and adding `@tmonfre` as a reviewer.

1. Ensure you have `python3` installed on your operating system.
2. Clone this repository.
3. Create a virtual environment:

    ```shell
    python3 -m venv ./venv
    source venv/bin/activate
    pip3 install -r requirements.txt
    ```

4. Run `./cli.py` to test
5. Run `./build.sh` to build the package. An executable named `zoom` will be generated in the `dist` directory. Move this to somewhere on your `$PATH` to run the command globally.
    - Note: Running this script will also generate a `dist/zoom.tar.gz` file with the zipped contents of `dist/zoom`. The script will output a SHA-256 hash of this file in the terminal. This is used for deployment to Homebrew.
