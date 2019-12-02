using System;
using System.Text;
using System.Diagnostics;

public static class Shell {

    private static StringBuilder m_builder = new StringBuilder();

    public static void StartBat(string batPath, params object[] args) {
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = batPath;
        if (args != null) {
            m_builder.Clear();
            for (ushort index = 0; index < args.Length; index++) {
                m_builder.Append(args[index]);
                m_builder.Append(',');
            }
            m_builder.Remove(m_builder.Length - 1, 1);
            info.Arguments = m_builder.ToString();
        }
        Process process = new Process();
        process.StartInfo = info;
        try {
            process.Start();
            process.WaitForExit();
        } catch (Exception exception) {
            UnityEngine.Debug.LogError("Shell::StartBat exception " + exception);
        } finally {
            process.Close();
        }
    }
}