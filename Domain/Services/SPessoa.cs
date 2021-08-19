﻿using System;
using System.Collections.Generic;
using System.Linq;
using DesafioGlobaltec.Domain.Data;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Domain.Services {
    public class SPessoa {
        private CatalogoDbContext _context;

        public SPessoa(CatalogoDbContext context) {
            _context = context;
        }

        public Pessoa Obter(string CodigoPessoa) {
            CodigoPessoa = CodigoPessoa?.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(CodigoPessoa)) {
                return _context.Pessoas.Where(
                    p => p.CodigoPessoa == CodigoPessoa
                ).FirstOrDefault();
            } else {
                return null;
            }
        }

        public IEnumerable<Pessoa> ListarTodos() {
            return _context.Pessoas.OrderBy(p => p.NomePessoa).ToList();
        }

        public Resultado Incluir(Pessoa dadosPessoa) {
            Resultado resultado = DadosValidos(dadosPessoa);
            resultado.Acao = "Inclusão de Cadastro";

            if (resultado.Inconsistencias.Count == 0 && _context.Pessoas.Where(
                p => p.CodigoPessoa == dadosPessoa.CodigoPessoa
            ).Count() > 0) {
                resultado.Inconsistencias.Add(
                    "Código de pessoa já cadastrado"
                );
            }

            if (resultado.Inconsistencias.Count == 0) {
                _context.Pessoas.Add(dadosPessoa);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(Pessoa dadosPessoa) {
            Resultado resultado = DadosValidos(dadosPessoa);
            resultado.Acao = "Atualização de Cadastro";

            if (resultado.Inconsistencias.Count == 0) {
                Pessoa pessoa = _context.Pessoas.Where(
                    p => p.CodigoPessoa == dadosPessoa.CodigoPessoa
                ).FirstOrDefault();

                if (pessoa == null) {
                    resultado.Inconsistencias.Add(
                        "Cadastro não encontrado")
                    ;
                } else {
                    pessoa.NomePessoa = dadosPessoa.NomePessoa;
                    //pessoa.Preco = dadosPessoa.Preco;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(string CodigoPessoa) {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de cadastro";

            Pessoa pessoa = Obter(CodigoPessoa);
            if (pessoa == null) {
                resultado.Inconsistencias.Add(
                    "Cadastro não encontrado"
                );
            } else {
                _context.Pessoas.Remove(pessoa);
                _context.SaveChanges();
            }

            return resultado;
        }

        private Resultado DadosValidos(Pessoa pessoa) {
            var resultado = new Resultado();
            if (pessoa == null) {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do cadastro"
                );
            } else {
                if (String.IsNullOrWhiteSpace(pessoa.CodigoPessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha o Código de cadastro"
                    );
                }

                if (String.IsNullOrWhiteSpace(pessoa.NomePessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha o Nome da pessoa"
                    );
                }

                //if (pessoa.Preco <= 0) {
                //    resultado.Inconsistencias.Add(
                //        "O Preço do Produto deve ser maior do que zero"
                //    );
                //}
            }

            return resultado;
        }
    }
}