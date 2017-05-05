using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


class Dialogue
{

    enum Type
    {
        I,
        S,
        P,
        Y,
        N
    };
    
    public string intro;
    
    //              type                    player      npc      true    false
    public Dictionary<string, KeyValuePair<string, KeyValuePair<string, string>>> convDictionary;

    public int yetiNum = 0;
    public int predNum = 0;
    public int smallNum = 0;



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
                    case "I":
                        t = Type.I;
                        break;
                    case "S":
                        t = Type.S;
                        break;
                    case "P":
                        t = Type.P;
                        break;
                    case "Y":
                        t = Type.Y;
                        break;
                    default:
                        switch (t)
                        {
                            case Type.I:
                                intro = line;
                                break;
                            case Type.S:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                convDictionary.Add("S" + smallNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, "ERROR")));
                                smallNum++;
                                break;
                            case Type.P:
                                playerD = line;
                                npcTrue = reader.ReadLine();
                                npcFalse = reader.ReadLine();
                                convDictionary.Add("P" + predNum,
                                    new KeyValuePair<string, KeyValuePair<string, string>>(playerD,
                                    new KeyValuePair<string, string>(npcTrue, npcFalse)));
                                predNum++;
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

