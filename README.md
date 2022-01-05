# Zoom CLI

Do you have multiple recurring Zoom meetings? Are you sick of having to open a calendar invite, find the meeting URL, then open it in your browser? This tool is for you.

`zoom` is a command line tool that allows users to store, access, and launch Zoom meetings on the fly.

It is written in Python and available to install via Homebrew. A previous version of this tool was written in C# with the .NET Core SDK.

## Usage

Below are the available commands. If an option/flag listed below is ommitted, you will be prompted to enter its value.

### Launch Meetings

- `zoom [url]` to launch any meeting on the fly.
- `zoom [name]` to launch a saved meeting by name.

### Save Meetings

- `zoom save` to save a new meeting
  - `-n, --name` for meeting name
  - `--url` for the meeting URL (must provide this or `--id`)
  - `--id` for the meeting ID (must provide this or `--url`)
  - `--password` for the meeting password (optional)

- `zoom edit` to edit a stored meeting
  - `-n, --name` for meeting name (optional)
  - `--url` for meeting URL (optional)
  - `--id` for meeting ID (optional)
  - `--password` for meeting password (optional)

- `zoom rm [name]` to delete a stored meeting

- `zoom ls` to see all stored meetings

## Installation Instructions

### Mac Users

1. Download and install Homebrew: [https://brew.sh](https://brew.sh).
2. `brew tap tmonfre/homebrew-tmonfre`
3. `brew install zoom`

### PC Users

This package is currently not yet available on Scoop. Please follow the developer instructions below in the meantime.

## Developer Instructions

Interested in contributing? Follow the steps below to install the project locally. Feel free to address any open issues, bug fixes, or feature requests by opening a pull request and adding `@tmonfre` as a reviewer.

1. Ensure you have `python3` installed on your operating system
2. Clone this repository
3. Run `./cli.py` to test
4. Run `./build.sh` to build the package. An executable named `zoom` will be generated in the `dist` directory. Move this to somewhere on your `$PATH` to run the command globally.
