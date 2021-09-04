#!/bin/bash

SCRIPT=$(readlink -f "$0")
SCRIPTPATH=$(dirname "$SCRIPT")
cd "$SCRIPTPATH"

rm -rf PixelPerfect/bin/
rm -rf PixelPerfect/obj/
