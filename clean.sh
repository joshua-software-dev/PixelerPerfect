#!/bin/bash

SCRIPT=$(readlink -f "$0")
SCRIPTPATH=$(dirname "$SCRIPT")
cd "$SCRIPTPATH"

rm -rf PixelerPerfect/bin/
rm -rf PixelerPerfect/obj/
