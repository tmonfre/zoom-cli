#!/bin/bash
pyinstaller cli.py --name zoom

cd ./dist/zoom
tar zcvf zoom.tar.gz zoom
cd ../../

sha256="$(shasum -a 256 ./dist/zoom/zoom.tar.gz | awk '{printf $1}')"

echo ""
echo "#####################################"
echo "Generated SHA-256:"
echo $sha256
echo "#####################################"
echo ""
