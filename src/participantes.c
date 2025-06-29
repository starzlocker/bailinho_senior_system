#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include <stdbool.h>
#include "../include/participantes.h"
#include "../utils/utils.h"

#define MAX_PARTICIPANTES 100

/**
 * @brief Lê os participantes do arquivo e armazena em um vetor
 * 
 * @param participantes Vetor de participantes para armazenar os dados
 * @param max_participantes Número máximo de participantes que o vetor pode armazenar
 * @return int Retorna o número de participantes lidos ou -1 em caso de erro ao abrir o arquivo
 */
int carregar_participantes(Participante *participantes, int max_participantes) {
    FILE *fp = fopen("../../data/participantes.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de participantes.\n\n");
        return -1;
    }

    int count = 0;
    while (fscanf(fp, "%49s %49s %11s %14s %d |", participantes[count].nome, participantes[count].sobrenome, 
                  participantes[count].cpf, participantes[count].celular, &participantes[count].idade) == 5) {
        count++;
        if (count >= max_participantes) {
            printf("Aviso: Número máximo de participantes (%d) atingido. Alguns participantes podem não ser carregados.\n\n", max_participantes);
            break;
        }
    }

    fclose(fp);
    return count;
}

/**
 * @brief Cadastra múltiplos participantes no sistema a partir de um vetor
 * 
 * Recebe um vetor de participantes e salva cada um no arquivo de participantes.
 * 
 * @param participantes Vetor de participantes a serem cadastrados (terminado com um marcador vazio)
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int cadastrar_participantes(Participante *participantes) {
    FILE *fp;

    // Abre o arquivo para adicionar novos participantes
    fp = fopen("../../data/participantes.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de participantes.\n\n");
        return 1;
    }

    // Grava cada participante no arquivo até encontrar um marcador vazio
    int count = 0;
    while (strlen(participantes[count].cpf) > 0) { // Verifica se o CPF não está vazio
        fprintf(fp, "%s %s %s %s %d |\n", participantes[count].nome, participantes[count].sobrenome, participantes[count].cpf, 
                participantes[count].celular, participantes[count].idade);
        count++;
    }

    fclose(fp);
    printf("%d participantes cadastrados com sucesso!\n\n", count);
    return 0;
}

/**
 * @brief Cadastra um novo participante no sistema
 * 
 * Solicita ao usuário informações sobre o novo participante, valida cada entrada e salva no arquivo.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int participantes_create() {
    Participante participante;
    FILE *fp;

    // Abre o arquivo para adicionar novos participantes
    fp = fopen("../../data/participantes.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo de participantes.\n\n");
        return 1;
    }

    // Solicita e valida o nome do participante
    bool nome_valido;
    do {
        printf("Digite o nome: ");
        scanf(" %49s", participante.nome);

        nome_valido = !validar_tamanho_string(participante.nome, 3, 49);
        if (!nome_valido) {
            printf("Nome inválido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!nome_valido);

    pretty_format_name(participante.nome, strlen(participante.nome));

    // Solicita e valida o sobrenome do participante
    bool sobrenome_valido;
    do {
        printf("Digite o sobrenome do participante: ");
        scanf("%49s", participante.sobrenome);

        sobrenome_valido = !validar_tamanho_string(participante.sobrenome, 3, 49);
        if (!sobrenome_valido) {
            printf("Sobrenome inválido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!sobrenome_valido);

    pretty_format_name(participante.sobrenome, strlen(participante.sobrenome));

    // Solicita e valida o CPF do participante
    bool cpf_valido;
    do {
        printf("Digite o CPF do participante (11 numeros): ");
        scanf("%11s", participante.cpf);

        cpf_valido = validate_cpf(participante.cpf, strlen(participante.cpf)) == 1;
        if (!cpf_valido) {
            printf("CPF inválido ou já cadastrado! Digite outro CPF.\n\n");
        }
    } while (!cpf_valido);

    // Solicita e valida o celular do participante
    bool celular_valido;
    do {
        printf("Digite o celular do participante (10 ou mais números): ");
        scanf("%14s", participante.celular);

        int tamanho_celular = strlen(participante.celular);
        celular_valido = tamanho_celular >= 10 && is_valid_number(participante.celular, tamanho_celular);
        
        if (!celular_valido) {
            printf("Celular inválido! Deve conter 10 ou mais números.\n\n");
        }
    } while (!celular_valido);

    // Solicita e valida a idade do participante
    bool idade_valida;
    do {
        printf("Digite a idade do participante: ");
        scanf("%d", &participante.idade);

        idade_valida = participante.idade >= 0 && participante.idade <= 120;
        if (!idade_valida) {
            printf("Idade inválida! Deve ser um número entre 0 e 120.\n\n");
        }
    } while (!idade_valida);

    Participante participantes[1] = {participante};

    // Grava o participante no arquivo
    bool cadastrou_com_sucesso = cadastrar_participantes(participantes);
    if (cadastrou_com_sucesso != 0) {
        printf("Erro ao cadastrar o participante no arquivo.\n\n");
        return 1;
    }

    printf("Participante cadastrado com sucesso!\n\n");
    pausar();
    limpar_tela();

    return 0;
}

/**
 * @brief Lista todos os participantes cadastrados
 * 
 * Lê os participantes do arquivo e exibe diretamente no console.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ao abrir o arquivo.
 */
int participantes_list() {
    Participante participantes[MAX_PARTICIPANTES]; // Vetor para armazenar até 100 participantes
    int count = 0;                                 // Contador para o número de participantes
    FILE *fp;

    // Abre o arquivo para leitura
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1) {
        return 1; // Erro ao abrir o arquivo
    }

    // Exibe o cabeçalho da tabela
    printf("\nLista de participantes:\n");
    printf("+--------------------------------------------------------------------------------------+\n");
    printf("| %-20s | %-20s | %-11s | %-14s | %-5s |\n", "Nome", "Sobrenome", "CPF", "Celular", "Idade");
    printf("+--------------------------------------------------------------------------------------+\n");

    // Exibe os participantes armazenados no vetor
    for (int i = 0; i < count; i++) {
        printf("| %-20s | %-20s | %-11s | %-14s | %-5d |\n", participantes[i].nome, participantes[i].sobrenome, 
               participantes[i].cpf, participantes[i].celular, participantes[i].idade);
    }
    printf("+--------------------------------------------------------------------------------------+\n");

    pausar();
    limpar_tela();
    return 0;
}

/**
 * @brief Atualiza os dados de um participante existente no sistema
 * 
 * Solicita o CPF do participante, permite ao usuário atualizar seus campos e salva as alterações no arquivo.
 * 
 * @return int Retorna 0 em caso de sucesso, 1 em caso de erro ou se o participante não for encontrado.
 */
int participantes_update() {
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1) {
        return 1; // Erro ao abrir o arquivo
    }

    char cpf_alvo[12];
    int indice_encontrado = -1;

    // Solicita o CPF do participante a ser atualizado
    bool cpf_valido;
    do {
    printf("Digite o CPF do participante que deseja atualizar: ");
        scanf("%11s", cpf_alvo);

        cpf_valido = validate_cpf(cpf_alvo, strlen(cpf_alvo)) != 0;
        if (!cpf_valido) {
            printf("CPF inválido ou não cadastrado! Digite outro CPF.\n\n");
        }
    } while (!cpf_valido);

    // Procura o participante no vetor
    for (int i = 0; i < count; i++) {
        if (strcmp(cpf_alvo, participantes[i].cpf) == 0) {
            indice_encontrado = i;
            break;
        }
    }

    // Atualiza os dados do participante encontrado
    printf("Atualizando dados do participante '%s %s'.\n\n", participantes[indice_encontrado].nome, participantes[indice_encontrado].sobrenome);

    // Atualiza o nome
    bool nome_valido;
    do {
        printf("Novo nome (atual: %s): ", participantes[indice_encontrado].nome);
        scanf(" %49[^\n]", participantes[indice_encontrado].nome);

        nome_valido = !validar_tamanho_string(participantes[indice_encontrado].nome, 3, 49);
        if (!nome_valido) {
            printf("Nome inválido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!nome_valido);

    pretty_format_name(participantes[indice_encontrado].nome, strlen(participantes[indice_encontrado].nome));

    // Atualiza o sobrenome
    bool sobrenome_valido;
    do {
        printf("Novo sobrenome (atual: %s): ", participantes[indice_encontrado].sobrenome);
        scanf(" %49[^\n]", participantes[indice_encontrado].sobrenome);

        sobrenome_valido = !validar_tamanho_string(participantes[indice_encontrado].sobrenome, 3, 49);
        if (!sobrenome_valido) {
            printf("Sobrenome inválido! Deve ter entre 3 e 50 caracteres.\n\n");
        }
    } while (!sobrenome_valido);

    pretty_format_name(participantes[indice_encontrado].sobrenome, strlen(participantes[indice_encontrado].sobrenome));

    // Atualiza o celular
    bool celular_valido;
    do {
        printf("Novo celular (atual: %s): ", participantes[indice_encontrado].celular);
        scanf("%14s", participantes[indice_encontrado].celular);

        celular_valido = strlen(participantes[indice_encontrado].celular) >= 10 && is_valid_number(participantes[indice_encontrado].celular, strlen(participantes[indice_encontrado].celular));
        if (!celular_valido) {
            printf("Celular inválido! Deve conter 10 ou mais números.\n\n");
        }
    } while (!celular_valido);

    // Atualiza a idade
    bool idade_valida;
    do {
        printf("Nova idade (atual: %d): ", participantes[indice_encontrado].idade);
        scanf("%d", &participantes[indice_encontrado].idade);

        idade_valida = participantes[indice_encontrado].idade >= 0 && participantes[indice_encontrado].idade <= 120;
        if (!idade_valida) {
            printf("Idade inválida! Deve ser um número entre 0 e 120.\n\n");
        }
    } while (!idade_valida);

    // Sobrescreve o arquivo com os dados atualizados
    bool cadastrou_com_sucesso = cadastrar_participantes(participantes);
    if (cadastrou_com_sucesso != 0) {
        printf("Erro ao salvar as alterações no arquivo.\n\n");
        return 1;
    }

    printf("Dados do participante atualizados com sucesso!\n\n");
    pausar();
    limpar_tela();
    return 0;
}


/**
 * @brief Apaga um participante específico pelo CPF
 *
 * Solicita o CPF do participante a ser removido e o exclui do arquivo.
 * Utiliza um arquivo temporário para a operação de exclusão.
 *
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_delete() {
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1) {
        return 1; // Erro ao abrir o arquivo
    }

    char cpf_alvo[12];
    int indice_encontrado = -1;

    // Solicita o CPF do participante a ser removido
    printf("Digite o CPF do participante que deseja apagar: ");
    scanf("%11s", cpf_alvo);

    // Procura o participante no vetor
    for (int i = 0; i < count; i++) {
        if (strcmp(participantes[i].cpf, cpf_alvo) == 0) {
            indice_encontrado = i;
            break;
        }
    }

    if (indice_encontrado == -1) {
        printf("Participante com CPF %s não encontrado.\n\n", cpf_alvo);
        pausar();
        limpar_tela();
        return 1;
    }

    // Remove o participante do vetor deslocando os elementos seguintes
    for (int i = indice_encontrado; i < count - 1; i++) {
        participantes[i] = participantes[i + 1];
    }
    count--; // Reduz o número de participantes

    // Sobrescreve o arquivo com os dados atualizados
    bool cadastrou_com_sucesso = cadastrar_participantes(participantes);
    if (cadastrou_com_sucesso != 0) {
        printf("Erro ao remover participante no arquivo.\n\n");
        return 1;
    }

    printf("Participante com CPF %s removido com sucesso!\n\n", cpf_alvo);
    pausar();
    limpar_tela();
    return 0;
}


/**
 * @brief Verifica se um participante existe pelo CPF
 *
 * @param cpf O CPF do participante a ser verificado
 * @return int 1 se o participante existir, 0 caso contrário
 */
int participante_existe(const char *cpf) {
    Participante participantes[MAX_PARTICIPANTES];
    int count = carregar_participantes(participantes, MAX_PARTICIPANTES);

    if (count == -1) {
        return 0; // Erro ao abrir o arquivo
    }

    for (int i = 0; i < count; i++) {
        if (strcmp(participantes[i].cpf, cpf) == 0) {
            return 1; // Participante encontrado
        }
    }

    return 0; // Participante não encontrado
}
