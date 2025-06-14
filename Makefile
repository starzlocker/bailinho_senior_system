# Makefile para o Sistema Bailinho Senior

CC = gcc
CFLAGS = -Wall -Wextra -g -O0

# Arquivos fonte
SRC = main.c src/participantes.c src/eventos.c src/produtos.c src/vendas.c utils/utils.c

# Arquivos de cabeçalho
HEADERS = include/participantes.h include/eventos.h include/produtos.h include/vendas.h utils/utils.h

# Nome do executável
TARGET = bailinho_senior_system

all: $(TARGET)

# Regra de dependência para recompilar quando os headers mudam
$(TARGET): $(SRC) $(HEADERS)

$(TARGET): $(SRC)
	$(CC) $(CFLAGS) -o $@ $^

clean:
	rm -f $(TARGET)

run: $(TARGET)
	./$(TARGET)

buildRun: clean all run

.PHONY: all clean run
