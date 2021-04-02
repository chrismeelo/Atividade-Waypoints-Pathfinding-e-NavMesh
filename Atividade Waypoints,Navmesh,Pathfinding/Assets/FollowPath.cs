using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    //objeto alvo
    Transform goal;

    //variavel que da a velocidade do objeto
    float speed = 5.0f;

    //variavel que calcula a distancia para o ponto
    float accuracy = 1.0f;

    //variavel da velocidade da rotação
    float rotSpeed = 2.0f;

    //variavel pega o wpmanager na cena
    public GameObject wpManager;

    //array dos pontos
    GameObject[] wps;

    //variavel que mostra o nó atual
    GameObject currentNode;

    //variavel do ponto atual
    int currentWP = 0;

    //variavel para o graph
    Graph g;

    void Start()
    {
        //pegando as variaveis que nos interessam nos scripts
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;

        //definindo o nó atual atual como 0
        currentNode = wps[0];
    }

    // Método que faz o tank ir ao heliporto
    public void GoToHeli()
    {
        //faz o caminho até o objetivo
        g.AStar(currentNode, wps[9]);
        //define para 0 novamente
        currentWP = 0;
    }

    // Método que faz o tank  ir as ruinas
    public void GoToRuin()
    {
        //faz o caminho até o objetivo
        g.AStar(currentNode, wps[3]);
        //define para 0 novamente
        currentWP = 0;
    }

void LateUpdate()
    {
        //Utilizando o metodo do graph
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
          return;

        //checa o nó mais proximo
        currentNode = g.getPathPoint(currentWP);

        //calcula a distancia de dois vetores, e se for menor que a accuracy definida, soma mais 1 no ponto atual
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,transform.position) < accuracy)
        {
            currentWP++;
        }

        //verifica se o ponto atual é menor que o g.getPathLength
        if (currentWP < g.getPathLength())
        {
            //define o objetivo
            goal = g.getPathPoint(currentWP).transform;
            //olha para o objetivo
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y,goal.position.z);
            //vai até o objetivo
            Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * rotSpeed);
        }
        //faz o player se mover
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
