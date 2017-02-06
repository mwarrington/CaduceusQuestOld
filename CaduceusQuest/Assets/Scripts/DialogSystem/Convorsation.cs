using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convorsation
{
    public string Name;
    public char Index;
    public List<Line> MyLines = new List<Line>();
    public List<DialogOptions> MyDialogOptionsArray = new List<DialogOptions>();

    private Line _currentLine = new Line();
    private string _currentName = "";
    private bool _readingName = false,
                 _readingIndex = false,
                 _writingSpeaker = false,
                 _writingEmotion = false,
                 _writingLineText = false,
                 _skipConvo = false,
                 _correctConvo = false;

    public Convorsation(string name, char index)
    {
        Name = name;
        Index = index;    

        PopulateConversation();
    }

    public void PopulateConversation()
    {
        string masterText = GameObject.Find("DialogHandler").GetComponent<DialogHandler>().MasterText.text;
        char currentChar;

        for (int i = 0; i < masterText.Length; i++)
        {
            currentChar = masterText[i];

            if (_skipConvo)
            {
                if (currentChar == ';' && masterText[i + 1] == ';')
                {
                    _skipConvo = false;
                    continue;
                }
                else
                    continue;
            }

            if (_readingName)
            {
                if (currentChar != '(')
                {
                    _currentName = _currentName + currentChar;
                }
                else if (_currentName == Name)
                {
                    _readingName = false;
                }
                else
                {
                    _readingName = false;
                    _skipConvo = true;
                    _currentName = "";
                    continue;
                }
            }

            //Section Finder
            if (!_correctConvo)
            {
                if (currentChar == '(')
                {
                    //Find Name
                    if (masterText[i + 1] == 'c')
                    {
                        i += 2;
                        _readingName = true;
                        continue;
                    }

                    //Find Index
                    if (masterText[i + 1] == 'i')
                    {
                        i += 3;
                        char index = masterText[i];
                        if (index == Index)
                        {
                            _correctConvo = true;
                            i++;
                            continue;
                        }
                    }
                }
            }
            else //Line and DO reader
            {
                //Line Finished
                if (currentChar == ';')
                {
                    if (masterText[i + 1] == ';')
                    {
                        MyLines.Add(_currentLine);
                        break;
                    }
                    else
                    {
                        MyLines.Add(_currentLine);
                        _currentLine = new Line();
                        _writingLineText = false;
                        continue;
                    }
                }

                if (currentChar == '|')
                {
                    _writingSpeaker = false;
                    _writingEmotion = false;
                    continue;
                }

                //Writing Speaker
                if (_writingSpeaker)
                {
                    _currentLine.Speaker = _currentLine.Speaker + currentChar;
                    continue;
                }

                //Writing Emotion
                if (_writingEmotion)
                {
                    _currentLine.EmotionColor = new Vector2((int)char.GetNumericValue(currentChar), (int)char.GetNumericValue(masterText[i + 2]));
                    i += 2;
                    continue;
                }

                //Writing Line Text
                if(_writingLineText)
                {
                    _currentLine.LineText = _currentLine.LineText + currentChar;
                    continue;
                }

                //Section Finder 2
                if (currentChar == '(')
                {
                    //Speaker
                    if (masterText[i + 1] == 's')
                    {
                        i += 2;

                        _writingSpeaker = true;
                        continue;
                    }

                    //Emotion
                    if(masterText[i + 1] == 'e')
                    {
                        i += 2;

                        _writingEmotion = true;
                        continue;
                    }

                    //Line Text
                    if (masterText[i + 1] == 't')
                    {
                        i += 2;

                        _writingLineText = true;
                        continue;
                    }
                }
            }
        }
    }
}