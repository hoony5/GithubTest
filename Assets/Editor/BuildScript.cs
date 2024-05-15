using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray(); // 빌드할 씬의 경로를 설정하세요
        string buildPath = "build/YourApp.apk"; // 빌드 결과물의 경로를 설정하세요

        string buildDirectory = Path.GetDirectoryName(buildPath);
        if (!Directory.Exists(buildDirectory))
        {
            Directory.CreateDirectory(buildDirectory);
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Android-specific settings
        EditorUserBuildSettings.buildAppBundle = false; // .aab 대신 .apk 로 빌드하려면 false 로 설정
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle; // 빌드 시스템 설정

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Console.WriteLine("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            throw new Exception("Build failed");
        }
    }
}