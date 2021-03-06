﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryNetwork
{
    public static class QLearningSolver
    {
        //public static float alpha = 1F;
        public static float reward;
        //public static float gamma = 1F;
        public static float bestExpectedValue;
        public static float QValue;


        public static void updateRewards(ref List<Node> nodes, int baseNode, int nextNode, float alpha, float gamma)
        {
            if(isLastNode(nodes,nextNode))
                setLastNodeRewards();
            else
                setRewards(nodes,nextNode);

            QValue = alpha * (reward + gamma * bestExpectedValue);
            nodes[baseNode].nextSystemsValues[nextNode] = QValue;
        }

        private static bool isLastNode(List<Node> nodes, int nextNode)
        {
            if (nextNode == (nodes.Count - 1))
                return true;
            else
                return false;
        }

        private static void setLastNodeRewards()
        {
            bestExpectedValue = 0;
            reward = 1;
        }

        private static void setRewards(List<Node> nodes, int nextNode)
        {
            bestExpectedValue = nodes[nextNode].getMaximumReward();
            if ((!nodes[nextNode].infQueue()) && (nodes[nextNode].customerCount == (nodes[nextNode].queueMaxSize + nodes[nextNode].serviceChannelsNumber)))
                reward = -1000F;
            else
                reward = 0F;
            reward += -((nodes[nextNode].customerCount - nodes[nextNode].serviceChannelsNumber) * nodes[nextNode].checkoutTime)-0.1F;
        }

    }
}
