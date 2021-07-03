using System.Diagnostics;

using UnityEditor;

namespace Editor.Build {

    public static class Builder {

        private static readonly string CLIENT_PATH = "Build/Client";
        private static readonly string SERVER_PATH = "Build/Server";
        private static readonly string ANDROID_PATH = "Build/Android";
        private static readonly string NAME = "chuzaman";
        private static readonly string DOCKER = $"docker build --pull --rm -f \"Dockerfile\" --build-arg EXECUTABLE=\"{NAME}.x86_64\" -t blazekill/{NAME}:latest \".\"";
        
        [MenuItem("Build/All")]
        public static void BuildAll() {
            ClientBuild();
            AndroidBuild();
            ServerBuild();

            EditorUtility.RevealInFinder(SERVER_PATH);
        }
        
        [MenuItem("Build/Client")]
        public static void ClientBuild() {
            var scenes = EditorBuildSettings.scenes;

            var clientOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(scenes),
                locationPathName = $"{CLIENT_PATH}/{NAME}.exe",
                target = BuildTarget.StandaloneWindows64,
                targetGroup = BuildTargetGroup.Standalone,
                options = BuildOptions.CompressWithLz4HC
            };

            BuildPipeline.BuildPlayer(clientOptions);
        }
        
        [MenuItem("Build/Android")]
        public static void AndroidBuild() {
            var scenes = EditorBuildSettings.scenes;

            var clientOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(scenes),
                locationPathName = $"{ANDROID_PATH}/{NAME}.apk",
                target = BuildTarget.Android,
                options = BuildOptions.CompressWithLz4HC
            };

            BuildPipeline.BuildPlayer(clientOptions);
        }
        
        [MenuItem("Build/Server")]
        public static void ServerBuild() {
            var scenes = EditorBuildSettings.scenes;

            var serverOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(scenes),
                locationPathName = $"{SERVER_PATH}/{NAME}.x86_64",
                target = BuildTarget.StandaloneLinux64,
                targetGroup = BuildTargetGroup.Standalone,
                options = BuildOptions.EnableHeadlessMode | BuildOptions.CompressWithLz4HC
            };

            var report = BuildPipeline.BuildPlayer(serverOptions);
            
            if (report.summary.totalErrors == 0 || report.summary.totalWarnings == 0) {
                DockerBuild();
            }
        }

        [MenuItem("Build/Docker")]
        public static void DockerBuild() {
            FileUtil.ReplaceFile("Dockerfile", "Build/Dockerfile");
            
            var shell = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "pwsh.exe",
                    WorkingDirectory = "Build",
                    RedirectStandardInput = true,
                    CreateNoWindow = false,
                    UseShellExecute = false
                }
            };
            
            shell.Start();
            
            shell.StandardInput.Write(DOCKER);
            shell.StandardInput.Flush();
            shell.StandardInput.Close();
            
            shell.WaitForExit();
            shell.Close();
        }

    }

}