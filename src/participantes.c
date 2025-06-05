#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include "../include/participantes.h"
#include "../utils/utils.h"

void flush_in() {
    int ch;
    do {
        ch = fgetc(stdin);
    } while (ch != EOF && ch != '\n');
}

int numero_is_valido(char str[], size_t length) {
    int valido = 1;
    for(int i = 0; i < length; i++) {
        if (str[i] < 48 || str[i] > 57) {
            valido = 0;
            break;
        }
    }
    return valido;
}

void normalize_str(char str[], size_t length) {
    char output[length];
    output[0] = toupper(str[0]);
    for (int i = 1; i < length; i++) {
        output[i] = tolower(str[i]);
    }
}

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
    scanf("%49s", participante.nome);
    flush_in();

    while (strlen(participante.nome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o nome do participante: ");
        scanf("%49s", participante.nome);
        flush_in();
    }
    
    printf("Digite o sobrenome do participante: ");
    scanf("%49s", participante.sobrenome);
    flush_in();

    while (strlen(participante.sobrenome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o sobrenome do participante: ");
        scanf("%49s", participante.sobrenome);
        flush_in();
    }

    printf("Digite o CPF do participante: ");
    scanf("%11s", participante.cpf);
    flush_in();

    while (strlen(participante.cpf) != 11 || !numero_is_valido(participante.cpf, strlen(participante.cpf))) {
        printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        scanf("%11s", participante.cpf);
        flush_in();
    }
    
    printf("Digite o celular do participante: ");
    scanf("%14s", participante.celular);
    flush_in();

    while (strlen(participante.celular) < 10 || !numero_is_valido(participante.celular, strlen(participante.celular))) {
        printf("\nO celular deve ser composto por 10 ou mais números! \nDigite o celular do participante: ");
        scanf("%14s", participante.celular);
        flush_in();
    }

    printf("Digite a idade do participante: ");
    scanf("%d", &participante.idade);
    flush_in();

    fprintf(fp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
    
    fclose(fp);
    //limpar_tela();
    
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
    printf("--------------------------------------------------------------------------------------\n");
    printf("| %-20s | %-20s | %-11s | %-14s | %-5s |\n", "Nome", "Sobrenome", "CPF", "Celular", "Idade");
    printf("--------------------------------------------------------------------------------------\n");

    while (fscanf(fp, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        printf("| %-20s | %-20s | %-11s | %-14s | %-5d |\n", 
               participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
    }

    printf("--------------------------------------------------------------------------------------\n");

    fclose(fp);
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
    scanf("%11s", cpf_alvo);
    getchar();

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
     // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        if (strcmp(cpf_alvo, participante.cpf) != 0) {
            // ERRO: Está faltando o participante.sobrenome aqui
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;
            printf("Digite o nome do participante (atual: %s): ", participante.nome);
            scanf("%49s", new_participante.nome);
            printf("Digite o sobrenome do participante (atual: %s): ", participante.sobrenome);
            scanf("%49s", new_participante.sobrenome);
            printf("Digite o CPF do participante (atual: %s): ", participante.cpf);
            scanf("%11s", new_participante.cpf);
            printf("Digite o celular do participante (atual: %s): ", participante.celular);
            scanf("%14s", new_participante.celular);
            printf("Digite a idade do participante (atual: %d): ", participante.idade);
            scanf("%d", &new_participante.idade);
            fprintf(fp_temp, "%s %s %s %s %d |\n", new_participante.nome, new_participante.sobrenome, new_participante.cpf, new_participante.celular, new_participante.idade);
            getchar();
        }
    }
    fclose(fp_original);
    fclose(fp_temp);

    remove("data/participantes.txt");
    rename("data/temp.txt", "data/participantes.txt");

    if (encontrado == 0) {
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
        //pausar();
        //limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!");
    //mpar_tela();
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
    scanf("%11s", cpf_alvo);
    getchar();

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
    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        // Se o CPF não for o que estamos procurando, escreve no arquivo temporário
        if (strcmp(participante.cpf, cpf_alvo) != 0) {
            // ERRO: Está faltando o participante.sobrenome aqui
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
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
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
    }

    //pausar();
    //limpar_tela();
    
    return 0;
}
