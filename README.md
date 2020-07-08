# Zoom Command Line Tool

Do you have multiple recurring Zoom meetings? Maybe a daily stand-up, or a bi-weekly class/meeting? Are you sick of having to open a calendar invite, find the meeting URL, then open it in your browser? Then this tool is for you.

`zoom` is a command line tool that allows users to store, access, and launch Zoom meetings on the fly. It is written in C# and uses the .NET Core SDK.

## Usage

Below are the available commands. If an option/flag listed below is ommitted, you will be prompted to enter its value.

### Launch Meetings

Run `zoom [url]` to launch any meeting on the fly.

Run `zoom launch [name]` to launch a saved meeting by name.

### Store Meetings

Run `zoom new` to store a new meeting

- `-n, --name` for the name the meeting should be referenced by
- `--id` for the meeting ID
- `-p, --password` for the meeting password (optional)

Run `zoom update` to update a stored meeting

- `-n, --name` for the name the meeting should be referenced by
- `--id` for the meeting ID
- `-p, --password` for the meeting password (optional)

Run `zoom delete` to delete a stored meeting

- `-n, --name` for the name the meeting should be referenced by

Run `zoom list` to see all stored meetings

## Installation Instructions

### Mac Users

1. Download and install .NET Core 3.1: https://dotnet.microsoft.com/download
2. Download and install Homebrew https://brew.sh/
3. `brew tap tmonfre/homebrew-tmonfre`
4. `brew install zoom`

### PC Users

This package is currently not yet available on Scoop. Please follow the developer instructions below in the meantime.

## Developer Instructions

Interested in contributing? Follow the steps below to install the project locally. Feel free to address any open issues, bug fixes, or feature requests by opening a pull request and adding `@tmonfre` as a reviewer.

1. Download and install .NET Core 3.1: https://dotnet.microsoft.com/download
2. Clone this repository
3. Run `make install` within the repository.
