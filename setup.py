import pathlib
from setuptools import setup
from zoom_cli import __version__

# adopted from: https://github.com/realpython/reader/blob/master/setup.py
README = (pathlib.Path(__file__).resolve().parent / "README.md").read_text()

# adopted from: https://stackoverflow.com/questions/6947988/when-to-use-pip-requirements-file-versus-install-requires-in-setup-py
REQUIREMENTS = [i.strip().split("=")[0] for i in open("requirements.txt").readlines()]

setup(
    name="zoom_cli",
    version=__version__,
    description="Save and launch Zoom meetings from the command line",
    long_description=README,
    long_description_content_type="text/markdown",
    url="https://github.com/tmonfre/zoom-cli",
    author="Thomas Monfre",
    author_email="tmonfre1@gmail.com",
    license="MIT",
    classifiers=[
        "License :: OSI Approved :: MIT License",
        "Programming Language :: Python",
        "Programming Language :: Python :: 2",
        "Programming Language :: Python :: 3",
    ],
    packages=["zoom_cli"],
    include_package_data=True,
    install_requires=REQUIREMENTS,
    entry_points={"console_scripts": ["zoom_cli=zoom_cli.__main__:main"]},
)