# Zoom Command Line Tool

Command line tool written in C# using the .NET Core SDK to store and launch zoom meetings on the fly.

## Usage

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

Run `zoom launch [name]` to launch a meeting

## Installation Instructions

1. Download and install .NET Core 3.1: https://dotnet.microsoft.com/download
2. Clone this repository
3. Run `make install` within the repository.
