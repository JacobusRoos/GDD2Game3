using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


class Dialogue
{

    enum Type
    {
        S,
        T,
        L,
        Y,
        N
    };
    //              type                    player      npc      true    false
    public Dictionary<string, KeyValuePair<string, KeyValuePair<string, string>>> convDictionary;

    private int yetiNum = 0;
    private int trueNum = 0;
    private int falseNum = 0;
    private int smallNum = 0;



    public void ParseDialogue(string NPCName)
    {
        convDictionary = new Dictionary<string, KeyValuePair<string, KeyValuePair<string, string>>>();

        StreamReader reader;

        string line = "";

        string playerD = "";
        string npcTrue = "";
        string npcFalse = "";
        Type t = Type.N;

        using (reader = new StreamReader("Assets/Dialogue/" + NPCName + ".txt"))
        {
            while ((line = reader.ReadLine()) != null)
            {
                switch (line)
                {
                    case "S":
                        t = Type.S;
                        break;
                    case "T":
                        t = Type.T;
                        break;
                    case "L":
                        t = Type.L;
                        break;
                    case "Y":
                        t = Type.Y;
                        break;
                    default:
                        switch (t)
                        {
                            case Type.S:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                convDictionary.Add("S" + smallNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, "ERROR")));
                                smallNum++;
                                break;
                            case Type.T:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                npcFalse = reader.ReadLine();
                                convDictionary.Add("T" + trueNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, npcFalse)));
                                trueNum++;
                                break;
                            case Type.L:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                npcFalse = reader.ReadLine();
                                convDictionary.Add("F" + falseNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, npcFalse)));
                                falseNum++;
                                break;
                            case Type.Y:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                npcFalse = reader.ReadLine();
                                convDictionary.Add("Y" + yetiNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, npcFalse)));
                                yetiNum++;
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }
    }

}

