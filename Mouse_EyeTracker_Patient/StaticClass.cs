using System;

namespace namespaceProgram
{
    class StaticClass
    {
      

        public static String firstMessage = "Keep the cursor inside each rectangle for at least 2s";
        public static String secondMessage = "Now try to blink\n"
                                           + "(close your eyes and open them when you hear the \" beep \" sound)";
        public static String thirdMessage = "Nice, you may have notice, the cursor had just freezed, \n"
                                          + "Now try to look at the biggest panel, then blink \n"
                                          + "(close your eyes and open them when you hear the \" beep \" sound)";
        public static String forthMessage = "Real good, you know, blinking while the cursor is frozen does a left click \n" +
                                            "Go do a left click on each panel \n" +
                                            "(blink one time to freeze the cursor, \n" +
                                            "then blink a second time while the cursor is frozen to click)";
        public static String fifthMessage = "You did it ! \n Now you can use this eye-tracked mouse ";

        public static Boolean isCursorOnPanel1 { get; set; } = false;
        public static Boolean isCursorOnPanel2 { get; set; } = false;
        public static Boolean isCursorOnPanel3 { get; set; } = false;
        public static Boolean isCursorOnPanel4 { get; set; } = false;

        public static Boolean isPanel1Activated { get; set; } = false;
        public static Boolean isPanel2Activated { get; set; } = false;
        public static Boolean isPanel3Activated { get; set; } = false;
        public static Boolean isPanel4Activated { get; set; } = false;

        public static Boolean isLabelAVisible { get; set; } = false;
        public static Boolean isLabelBVisible { get; set; } = false;
        public static Boolean isLabelCVisible { get; set; } = false;
        public static Boolean isLabelDVisible { get; set; } = false;

        public static Boolean isCircularProgressBar1Complete { get; set; } = false;
        public static Boolean isCircularProgressBar2Complete { get; set; } = false;
        public static Boolean isCircularProgressBar3Complete { get; set; } = false;
        public static Boolean isCircularProgressBar4Complete { get; set; } = false;

        public static Boolean isReadyToClick = false;

        public static int freezeCount = 0;

    }
}
