#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
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
    char cpf[12];
    char nome[50];
    char sobrenome[50];
    char is_cpf_valido = 0;


    fp = fopen("../../data/participantes.txt", "a+");
    if (fp == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    printf("Digite o nome do participante: ");
    scanf("%49s", nome);
    flush_in();

    while (strlen(nome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o nome do participante: ");
        scanf("%49s", nome);
        flush_in();
    }

    pretty_format_name(nome, strlen(nome));
    strcpy(participante.nome, nome);

    printf("Digite o sobrenome do participante: ");
    scanf("%49s", sobrenome);
    flush_in();

    while (strlen(sobrenome) < 3) {
        printf("\nO nome deve ter 3 ou mais letras! \nDigite o sobrenome do participante: ");
        scanf("%49s", sobrenome);
        flush_in();
    }

    pretty_format_name(sobrenome, strlen(sobrenome));
    strcpy(participante.sobrenome, sobrenome);

    printf("Digite o CPF do participante: ");
    scanf("%11s", cpf);
    flush_in();

    is_cpf_valido = validate_cpf(cpf, strlen(cpf));

    while (is_cpf_valido != 1) {
        if (is_cpf_valido == 0) {
            printf("\nO CPF já está cadastrado!\nDigite o CPF do participante: ");
        } else if (is_cpf_valido == -1) {
            printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        }
        scanf("%11s", cpf);
        is_cpf_valido = validate_cpf(cpf, strlen(cpf));
        flush_in();
    }

    strcpy(participante.cpf, cpf);

    printf("Digite o celular do participante: ");
    scanf("%14s", participante.celular);
    flush_in();

    while (strlen(participante.celular) < 10 || !is_valid_number(participante.celular, strlen(participante.celular))) {
        printf("\nO celular deve ser composto por 10 ou mais números! \nDigite o celular do participante: ");
        scanf("%14s", participante.celular);
        flush_in();
    }

    printf("Digite a idade do participante: ");
    scanf("%d", &participante.idade);
    flush_in();

    while (participante.idade < 0 || participante.idade > 120) {
        printf("\nA idade deve ser um número entre 0 e 120! \nDigite a idade do participante: ");
        scanf("%d", &participante.idade);
        flush_in();
    }

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

    fp = fopen("../../data/participantes.txt", "r");
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
    char nome[50];
    char sobrenome[50];
    int encontrado = 0;

    printf("Digite o CPF do participante que deseja atualizar: ");
    scanf("%11s", cpf_alvo);
    flush_in();
    while(strlen(cpf_alvo) != 11 || !is_valid_number(cpf_alvo, strlen(cpf_alvo))) {
        printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        scanf("%11s", cpf_alvo);
        flush_in();
    }


    fp_original = fopen("data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("../../data/temp.txt", "w");
    if (fp_temp == NULL) {
        printf("Erro ao criar arquivo temporário.\n");
        fclose(fp_original);
        return 1;
    }

    // Lê cada participante do arquivo original
    while (fscanf(fp_original, "%49s %49s %11s %14s %d |", participante.nome, participante.sobrenome, participante.cpf, participante.celular, &participante.idade) == 5) {
        if (strcmp(cpf_alvo, participante.cpf) != 0) {
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;

            printf("Digite o nome do participante (atual: %s): ", participante.nome);
            scanf("%49s", nome);
            flush_in();

            while (strlen(nome) < 3) {
                printf("\nO nome deve ter 3 ou mais letras! \nDigite o nome do participante: ");
                scanf("%49s", nome);
                flush_in();
            }
            pretty_format_name(nome, strlen(nome));
            strcpy(new_participante.nome, nome);

            printf("Digite o sobrenome do participante (atual: %s): ", participante.sobrenome);
            scanf("%49s", sobrenome);
            flush_in();

            while (strlen(sobrenome) < 3) {
                printf("\nO nome deve ter 3 ou mais letras! \nDigite o sobrenome do participante: ");
                scanf("%49s", sobrenome);
                flush_in();
            }
            pretty_format_name(sobrenome, strlen(sobrenome));
            strcpy(new_participante.sobrenome, sobrenome);

            printf("Digite o CPF do participante (atual: %s): ", participante.cpf);
            scanf("%11s", new_participante.cpf);
            flush_in();

            if (strcmp(new_participante.cpf, participante.cpf) != 0) {
                char cpf_valido = validate_cpf(new_participante.cpf, strlen(new_participante.cpf));
                while (cpf_valido != 1) {
                    if (cpf_valido == 0) {
                        printf("CPF já cadastrado! Digite outro CPF: ");
                    } else {
                        printf("CPF inválido! Digite um CPF com 11 números: ");
                    }
                    scanf("%11s", new_participante.cpf);
                    flush_in();
                    cpf_valido = validate_cpf(new_participante.cpf, strlen(new_participante.cpf));
                }
            }

            printf("Digite o celular do participante (atual: %s): ", participante.celular);
            scanf("%14s", new_participante.celular);
            flush_in();

            while (strlen(new_participante.celular) < 10 || !is_valid_number(new_participante.celular, strlen(new_participante.celular))) {
                printf("\nO celular deve ser composto por 10 ou mais números! \nDigite o celular do participante: ");
                scanf("%14s", new_participante.celular);
                flush_in();
            }
            printf("Digite a idade do participante (atual: %d): ", participante.idade);
            scanf("%d", &new_participante.idade);
            flush_in();

            while (new_participante.idade < 0 || new_participante.idade > 120) {
                printf("\nA idade deve ser um número entre 0 e 120! \nDigite a idade do participante: ");
                scanf("%d", &new_participante.idade);
                flush_in();
            }

            fprintf(fp_temp, "%s %s %s %s %d |\n", new_participante.nome, new_participante.sobrenome, new_participante.cpf, new_participante.celular, new_participante.idade);
        }
    }
    fclose(fp_original);
    fclose(fp_temp);

    remove("../../data/participantes.txt");
    rename("../../data/temp.txt", "data/participantes.txt");

    if (encontrado == 0) {
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
        //pausar();
        //limpar_tela();
        return 1;
    }

    printf("Dados atualizados com sucesso!\n");
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
    flush_in();
    while(strlen(cpf_alvo) != 11 || !is_valid_number(cpf_alvo, strlen(cpf_alvo))) {
        printf("\nO CPF deve ser composto por 11 números! \nDigite o CPF do participante: ");
        scanf("%11s", cpf_alvo);
        flush_in();
    }

    fp_original = fopen("../../data/participantes.txt", "r");
    if (fp_original == NULL) {
        printf("Erro ao abrir o arquivo.\n");
        return 1;
    }

    fp_temp = fopen("../../data/temp.txt", "w");
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
            fprintf(fp_temp, "%s %s %s %s %d |\n", participante.nome, participante.sobrenome, participante.cpf, participante.celular, participante.idade);
        } else {
            encontrado = 1;
            strcpy(nome_alvo, participante.nome);
        }
    }

    fclose(fp_original);
    fclose(fp_temp);

    // Exclui o arquivo original
    remove("../../data/participantes.txt");
    // Renomeia o arquivo temporário para o nome do arquivo original
    rename("../../data/temp.txt", "../../data/participantes.txt");

    if (encontrado) {
        printf("Participante %s com CPF %s removido com sucesso!\n", nome_alvo, cpf_alvo);

    } else {
        printf("\nParticipante com CPF %s não encontrado!\n", cpf_alvo);
    }

    //pausar();
    //limpar_tela();

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
    FILE *fp = fopen("../../data/participantes.txt", "r");
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
