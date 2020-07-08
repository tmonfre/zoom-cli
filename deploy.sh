#!/bin/bash

if [ $# -eq 0 ]; then
    echo "Please provide version to update"
    exit 1
fi

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

# upload to server
rm -rf ../personal-server/static/zoom.tar.gz
cp bin/zoom.tar.gz ../personal-server/static/zoom.tar.gz 
cd ../personal-server
git add -A 
git commit -m "update zoom.tar.gz"
git push
cd ../zoom

# uninstall via dotnet
make uninstall

# generate sha256 signature and update in homebrew repository
shatmp="    sha256 \"$(shasum -a 256 ../personal-server/static/zoom.tar.gz | awk '{printf $1}')\""
sed -i.bak "5s/.*/$shatmp/" ../homebrew-tmonfre/zoom.rb 
unset shatmp

# update versions in homebrew repository
versiontemp="    version \"$1\""
sed -i.bak "6s/.*/$versiontemp/" ../homebrew-tmonfre/zoom.rb 
unset versiontemp

# push homebrew repo
cd ../homebrew-tmonfre
git add -A
git commit -m "update sha256"
git push
cd ../zoom

# send done message
clear
echo "Successfully uploaded to server and updated homebrew tap"
