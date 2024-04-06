using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class User
{
    public string nome;
    public string data_nascimento;
    public string sexo;
    public string morada;
    public string carta_conducao;
    public string especialidade;
    public string email;

    public User()
    {
    }

    public User(string nome, string data_nascimento, string sexo, string morada, string carta_conducao, string especialidade, string email)
    {
        this.nome = nome;
        this.data_nascimento = data_nascimento;
        this.sexo = sexo;
        this.morada = morada;
        this.carta_conducao = carta_conducao;
        this.especialidade = especialidade;
        this.email = email;
    }
}