using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardinalDirections
{
	NONE = 0,
	FORWARD = 1,
	BACKWARD = 2,
	LEFT = 3,
	RIGHT = 4
}

public enum EncounterActionType
{
	DIALOG,
	EVENT,
	COMPSCI,
	DOCTOR,
	EPIDEMIOLOGIST,
	CHEMENG,
}

public enum DialogChangeType
{
    CONVOEND,
    ENCOUNTERWIN,
    ENCOUNTERFAIL,
    ITEMTRIGGER
}

public enum EncounterPattern
{
    ALTERNATE,
    DOUBLEALTERNATE,
    PLAYER1DIALOG2,
    PLAYER2DIALOG1
}

public enum EncounterMenus
{
    BASEMENU,
    SKILLSUBMENU,
    SSKILLSUBMENU,
    ESKILLSUBMENU,
    TSKILLSUBMENU,
    MSKILLSUBMENU,
    CSKILLSUBMENU
}

public enum SkillType
{
    SCIENCE,
    ENGINEERING,
    TECHNOLOGY,
    MATHAMATICS,
    COMMUNICATION
}

public enum EncounterTurnType
{
    PLAYER,
    EVENT,
    ALLY
}