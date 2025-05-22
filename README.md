# Playnite Startup Splash Screen Plugin

## 🚧 **Beta Release Available!**

[Download the Latest Release](https://github.com/aliceroherty/PlayniteStartupSplash/releases/download/v1.0.0-beta.1/StartupScreen_2b363259-a65d-4c5c-a044-11160be2cbb3_1_0.pext)

### Installation Instructions

Download the `.pext` file from the link above or [**Releases**](https://github.com/aliceroherty/PlayniteStartupSplash/releases) page on this repository. Double-click the file and Playnite should prompt you to install the extension.

## Overview

A plugin for Playnite to show a splash screen on startup until Playnite has had time to load. This will ensure the windows desktop is no longer shown while launching Playnite. Especially useful for Fullscreen mode when you want a console-like UX.

## Caveats

With the current implementation the default Playnite splash does show for ~2 seconds until the splash screen process can launch when cold booting Playnite.

The implementation that cause this added delay is the best way I have found so far to avoid having to override Playnite's exe with a wrapper launcher which would be hell to maintain but feel free to open an issue if you have any ideas that may help reduce the delay.

## Setup

The ideal environment and use case for this plugin would be to have Playnite run on Windows startup in the background and have the splash screen show when switching to fullscreen mode. I believe this will get the splash screen to show a bit quicker.

## Credits

### Inspired By

[![Static Badge](https://img.shields.io/badge/GitHub-tedhinklater%2Fplaynitesplashintro-white?style=for-the-badge&logo=github&logoColor=white&labelColor=black&color=%232e2e2e&link=https%3A%2F%2Fgithub.com%2Ftedhinklater%2Fplaynitesplashintro&link=https%3A%2F%2Fgithub.com%2Ftedhinklater%2Fplaynitesplashintro)](https://github.com/tedhinklater/playnitesplashintro)
