#ifndef VENDAS_H
#define VENDAS_H

/**
 * @struct Venda
 * @brief Estrutura que armazena os dados de uma venda
 */
typedef struct {
    char cpf_participante[12];
    char nome_evento[50];
    char nome_produto[50];
    int quantidade;
} Venda;

/**
 * @brief Cadastra uma nova venda no sistema
 * 
 * Solicita ao usuário informações sobre o participante, evento, produto e quantidade.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int vendas_create();

/**
 * @brief Lista todas as vendas cadastradas
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int vendas_list();

/**
 * @brief Apaga uma venda específica
 * 
 * Solicita o CPF do participante e o nome do evento para identificar a venda a ser removida.
 * 
 * @return int 0 em caso de sucesso, 1 em caso de erro
 */
int vendas_delete();

#endif /* VENDAS_H */
