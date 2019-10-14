namespace VFrame {
    public enum NScene {
        //	Everything = -1,
        //	Nothing = 0,
        Load = 1,
        Main = 2,
        Editor = 4,
        Play = 8,
        PlayPiano = 16,
        PlayFall = 32,
        Result = 64,
    }

    public enum NPage {
        FrameSelect,
        PanelInfo,
        PanelBlock,

        Tooltip,

        Load,

        Sample,

        Main,
        MainMusicList,
        Download,
        Vip,
        Login,
        Register,
        ForgotPassword,
        ChangeTheme,

        RhythmEditor,
        EditorSave,
        EditorBpm,
        EditorConvert,
        EditorNoteTime,
        EditorCreateNote,
        EditorLoadNote,
        EditorMusicList,
        EditorSendMail,

        RhythmPlay,
        PlayPiano,
        PlayFall,

        Result,
    }

    public enum UIMSG {
        Enable,
        Disable,
        DisableComplete,

        SetTipText,

        OperEnable,

        ChangeMusic,
        InitPadPlate,
        TransmitMusicIndex,

        ChangeNoteOffset,
    }

    public enum TableType {
        Localize,
        Hero,
        Monster,
        Card,
        Skill,
        Equip,
        Loot,

        Max
    }

}