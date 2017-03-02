using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convorsation
{
    public string Name;
    public char Index;
    public List<Line> MyLines = new List<Line>();
    public List<DialogOptions> MyDialogOptionsList = new List<DialogOptions>();

    private Line _currentLine = new Line();
    private string _currentName = "";
    private int _currentDOGroupIndex = -1,
                _currentDOIndex;
    private bool _readingName,
                 _writingSpeaker,
                 _writingEmotion,
                 _writingLineText,
                 _writingDialogOptionLine,
                 _skipConvo,
                 _correctConvo,
                 _skipLine;

    public Convorsation(string name, char index)
    {
        Name = name;
        Index = index;    

        PopulateConversation();
    }

    public void PopulateConversation()
    {
        string masterText = GameObject.Find("DialogHandler").GetComponent<DialogManager>().MasterText.text;
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

            if (_skipLine)
            {
                if (currentChar == '/' && masterText[i + 1] == '/')
                    _skipLine = false;
                else
                    continue;
            }
            else if (currentChar == '/' && masterText[i + 1] == '/')
            {
                _skipLine = true;
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
                        if (_currentDOGroupIndex == -1)
                        {
                            MyLines.Add(_currentLine);
                            _currentLine = new Line();
                            _writingLineText = false;
                        }
                        else if (masterText[i + 3] == '[')
                        {
                            _writingDialogOptionLine = false;
                            _currentDOGroupIndex = -1;
                            _currentDOIndex = 0;
                        }
                        else
                        {
                            _writingDialogOptionLine = false;
                            _currentDOIndex++;
                        }

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
                    _currentLine.MyEmotion = new Emotion(currentChar, (int)char.GetNumericValue(masterText[i + 2]));
                    i += 2;
                    continue;
                }

                //Writing Line Text
                if(_writingLineText)
                {
                    if (currentChar == '#')
                    {
                        _currentLine.LastLine = true;
                    }
                    else if(currentChar == '<')
                    {
                        i++;
                        _currentLine.NextGroupIndex = (int)char.GetNumericValue(masterText[i]);
                    }
                    else if (currentChar == '>')
                    {
                        i++;
                        _currentLine.NextLineIndex = (int)char.GetNumericValue(masterText[i]);
                    }
                    else
                        _currentLine.LineText = _currentLine.LineText + currentChar;

                    continue;
                }

                //Writing Dialog Option Line
                if(_writingDialogOptionLine)
                {
                    MyDialogOptionsList[_currentDOGroupIndex].myOptions[_currentDOIndex].DialogOptionText = MyDialogOptionsList[_currentDOGroupIndex].myOptions[_currentDOIndex].DialogOptionText + currentChar;
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

                    //Dialog Option group
                    if (masterText[i + 1] == 'g')
                    {
                        i += 3;

                        if(_currentDOGroupIndex == -1)
                        {
                            _currentDOGroupIndex = (int)char.GetNumericValue(masterText[i]);
                            MyDialogOptionsList.Add(new DialogOptions());
                        }

                        MyDialogOptionsList[_currentDOGroupIndex].myOptions.Add(new DialogOption());

                        continue;
                    }

                    //Next Line Index
                    if (masterText[i + 1] == 'n')
                    {
                        i += 2;

                        if (masterText[i + 2] == '(')
                            MyDialogOptionsList[_currentDOGroupIndex].myOptions[_currentDOIndex].MyNextLine = (int)char.GetNumericValue(masterText[i + 1]);
                        else
                        {
                            MyDialogOptionsList[_currentDOGroupIndex].myOptions[_currentDOIndex].MyNextLine = int.Parse("" + masterText[i + 1] + masterText[i + 2]);
                        }
                        
                        continue;
                    }

                    //Dialog Option Line
                    if (masterText[i + 1] == 'l')
                    {
                        i += 2;

                        _writingDialogOptionLine = true;
                        continue;
                    }
                }
            }
        }
    }
}