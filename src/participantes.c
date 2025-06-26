#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../include/participantes.h"
#include "../utils/utils.h"


/**
 * @brief Cadastra um novo participante no sistema
 * 
 * Solicita ao usuário informações sobre o novo participante e
 * as armazena em um arquivo de texto (participantes.txt).
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_create() {
    Participante participante;
    FILE *fp;

    fp = fopen("data/participantes.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("Digite o nome do participante: ");
    scanf("%s", participante.nome);
    printf("Digite o CPF do participante: ");
    scanf("%s", participante.cpf);
    printf("Digite o celular do participante: ");
    scanf("%s", participante.celular);
    printf("Digite a idade do participante: ");
    scanf("%d", &participante.idade);

    fprintf(fp, "%s %s %s %d |\n", participante.nome, participante.cpf, participante.celular, participante.idade);
    
    fclose(fp);
    limpar_tela();
    
    return 0;
}


/**
 * @brief Lista todos os participantes cadastrados
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int participantes_list() {
    Participante participante;
    FILE *fp;

    fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }


    printf("\nLista de participantes:\n");
    printf("╔══════════════════════════════════════════════════════════════════════════════════════╗\n");
    printf("║ %-40s ║ %-15s ║ %-15s ║ %-5s ║\n", "Nome", "CPF", "Celular", "Idade");
    printf("║--------------------------------------------------------------------------------------║\n");
    
    while (fscanf(fp, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
        printf("║ %-40s ║ %-15s ║ %-15s ║ %-5d ║\n", participante.nome, participante.cpf, participante.celular, participante.idade);
    }
    printf("╚══════════════════════════════════════════════════════════════════════════════════════╝\n");

    fclose(fp);
    pausar();
    limpar_tela();
    
    return 0;
}


/**
 * @brief Atualiza os dados de um participante específico
 * 
 * Solicita o CPF do participante a ser atualizado e, se encontrado,
 * permite a edição de todos os seus dados. Utiliza um arquivo temporário
 * para fazer a atualização.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro ou participante não encontrado
 */
int participantes_update() {
    FILE *fp_original, *fp_temp;
    Participante participante, new_participante;
    char cpf_alvo[12];
    int encontrado = 0;

    printf("Digite o CPF do participante que deseja atualizar: ");
    scanf("%s", cpf_alvo);

    fp_original = fopen("data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
        if (strcmp(cpf_alvo, participante.cpf) != 0) {
            fprintf(fp_temp, "%s %s %s %d |\n", participante.nome, participante.cpf, participante.celular, participante.idade);

        } else {
            encontrado = 1;
            printf("Digite o nome do participante (atual: %s): ", participante.nome);
            scanf("%s", new_participante.nome);
            printf("Digite o CPF do participante (atual: %s): ", participante.cpf);
            scanf("%s", new_participante.cpf);
            printf("Digite o celular do participante (atual: %s): ", participante.celular);
            scanf("%s", new_participante.celular);
            printf("Digite a idade do participante (atual: %d): ", participante.idade);
            scanf("%d", &new_participante.idade);
            fprintf(fp_temp, "%s %s %s %d |\n", new_participante.nome, new_participante.cpf, new_participante.celular, new_participante.idade);
            
        }
    }
    fclose(fp_original);
    fclose(fp_temp);

    remove("data/participantes.txt");
    rename("data/temp.txt", "data/participantes.txt");

    if (encontrado == 0) {
        printf("Participante nâo encontrado.\n");
        pausar();
        limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
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
    FILE *fp_original, *fp_temp;
    Participante participante;
    char cpf_alvo[12];
    int encontrado = 0;
    char nome_alvo[50];

    printf("Digite o CPF do participante que deseja apagar: ");
    scanf("%s", cpf_alvo);

    fp_original = fopen("data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
        // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
        if (strcmp(participante.cpf, cpf_alvo) != 0) {
            fprintf(fp_temp, "%s %s %s %d |\n", participante.nome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;
            strcpy(nome_alvo, participante.nome);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Exclui o arquivo original
    remove("data/participantes.txt");
    // Renomeia o arquivo temporário para o nome do arquivo original
    rename("data/temp.txt", "data/participantes.txt");

    if (encontrado) {
        printf("Participante %s com CPF %s removido com sucesso!\n", nome_alvo, cpf_alvo);

    } else {
        printf("\n⚠️ participante com CPF %s não encontrado! ⚠️\n", cpf_alvo);
    }

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
    Participante participante;
    FILE *fp = fopen("data/participantes.txt", "r");
    if (fp == NULL) {
        return 0;
    }

    while (fscanf(fp, "%49s %11s %14s %d |", participante.nome, participante.cpf, participante.celular, &participante.idade) == 4) {
        if (strcmp(participante.cpf, cpf) == 0) {
            fclose(fp);
            return 1;
        }
    }

    fclose(fp);
    return 0;
}
