CC = gcc
CFLAGS = -Wall -std=c99 -Iinclude

SRC = $(wildcard src/*.c)
OBJ = $(SRC:.c=.o)
OUT = build/raytracer

all: $(OUT)

$(OUT): $(SRC)
	$(CC) $(CFLAGS) -o $(OUT) $(SRC)

clean:
	rm -f build/*
