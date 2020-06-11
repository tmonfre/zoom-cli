clean:
	rm -rf ./nupkg
	clear

install:
	dotnet pack
	dotnet tool install --global --add-source ./nupkg zoom

uninstall:
	dotnet tool uninstall -g zoom
	make clean

update:
	make uninstall
	make install

deploy:
	./deploy.sh