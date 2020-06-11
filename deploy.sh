#!/bin/bash

# run dotnet installation
if hash zoom 2>/dev/null; then
    make update
else
    make install
fi

# create temp directory and copy over necessary files
mkdir ./zoom
mkdir ./zoom/.store
cp ~/.dotnet/tools/zoom ./zoom
cp -r ~/.dotnet/tools/.store/zoom ./zoom/.store

# generate tarball then remove temp directory/files
tar zcvf zoom.tar.gz zoom
rm -rf zoom
rm -rf bin/zoom.tar.gz

# move to bin (gitignored and will clear out on build)
mv zoom.tar.gz bin

# generate sha256
shasum -a 256 bin/zoom.tar.gz | awk '{printf $1}' | pbcopy

# upload to server
server upload bin/zoom.tar.gz

# uninstall via dotnet
make uninstall

# send done message
echo "Successfully deployed to server. sha256 signature copied to clipboard. Update sha256 field in zoom.rb in tmonfre/homebrew-tmonfre then push to GitHub for homebrew to reflect the changes."
