# Arquitetura do Projeto

Este documento define o padrão de organização e arquitetura do projeto **bailinho-senior-system** para garantir organização, escalabilidade e facilidade de manutenção.

## Estrutura de Diretórios

```
bailinho-senior-system/
│
├── main.c                // Função principal e menu principal
│
├── include/              // Headers (.h) com declarações de funções e structs
│   ├── eventos.h
│   ├── participantes.h
│   ├── produtos.h
│   └── vendas.h
│
├── src/                  // Implementações (.c) dos módulos
│   ├── eventos.c
│   ├── participantes.c
│   ├── produtos.c
│   └── vendas.c
│
├── menus/                // Menus de navegação de cada módulo
│   ├── eventos.c
│   ├── participantes.c
│   ├── produtos.c
│   └── vendas.c
│
├── utils/                // Funções utilitárias e helpers reutilizáveis
│   ├── utils.h
│   └── utils.c
│
├── data/                 // Dados persistentes (.txt)
│   ├── eventos.txt
│   ├── participantes.txt
│   ├── produtos.txt
│   └── vendas.txt
│
├── build/                // (Opcional) Arquivos compilados
│
├── Makefile              // (ou CMakeLists.txt) para build automatizado
└── README.md             // Documentação do projeto
```

## Descrição das Pastas e Arquivos

- **main.c**: Ponto de entrada do sistema, responsável pelo menu principal.
- **include/**: Headers (.h) com declarações de funções, structs e constantes de cada módulo.
- **src/**: Implementação das funcionalidades de cada módulo, separadas por domínio.
- **menus/**: Implementação dos menus de cada módulo, responsáveis pela interação com o usuário.
- **utils/**: Funções utilitárias e helpers reutilizáveis em todo o projeto (ex: validação, manipulação de strings, etc).
- **data/**: Armazena arquivos de dados persistentes.
- **build/**: (Opcional) Diretório para arquivos gerados na compilação.
- **Makefile/CMakeLists.txt**: Scripts para automatizar a compilação.
- **README.md**: Documentação geral do projeto.

## Convenções de Nomenclatura
- Arquivos de header: `modulo.h` (ex: `eventos.h`)
- Arquivos de implementação: `modulo.c` (ex: `eventos.c`)
- Funções: prefixadas pelo módulo e seguindo o padrão CRUD (em inglês):
  - `module_create` (Create)
  - `module_read` (Read)
  - `module_update` (Update)
  - `module_delete` (Delete)
  - Exemplo: `eventos_create`, `produtos_list`, `participantes_update`, `vendas_delete`
- Menus: arquivos em `menus/` com nome do módulo (ex: `menus/eventos.c`)

## Orientações para Modularização
- Cada módulo deve ter seu header em `include/` e implementação em `src/`.
- Funções de interação (menus) devem ficar em `menus/`.
- Dados persistentes devem ser lidos/salvos em arquivos em `data/`.
- Evite duplicação de código entre módulos.

## Exemplo: Adicionando um Novo Módulo
1. Crie `include/novo_modulo.h` com as declarações.
2. Implemente as funções em `src/novo_modulo.c`.
3. Crie o menu em `menus/novo_modulo.c`.
4. Adicione o arquivo de dados em `data/novo_modulo.txt` (se necessário).
5. Inclua o novo menu e funções no `main.c`.

---