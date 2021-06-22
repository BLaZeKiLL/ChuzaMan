using UnityEditor;

namespace Editor.Build {

    public static class Builder {

        private static readonly string CLIENT_PATH = "Build/Client";
        private static readonly string SERVER_PATH = "Build/Server";
        private static readonly string EXE_NAME = "ChuzaMan";
        
        [MenuItem("Build/Both")]
        public static void ClientServerBuild() {
            ClientBuild();
            ServerBuild();
        }
        
        [MenuItem("Build/Client")]
        public static void ClientBuild() {
            var scenes = EditorBuildSettings.scenes;

            var clientOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(scenes),
                locationPathName = $"{CLIENT_PATH}/{EXE_NAME}_Client.exe",
                target = BuildTarget.StandaloneWindows64
            };

            BuildPipeline.BuildPlayer(clientOptions);
        }
        
        [MenuItem("Build/Server")]
        public static void ServerBuild() {
            var scenes = EditorBuildSettings.scenes;

            var serverOptions = new BuildPlayerOptions {
                scenes = EditorBuildSettingsScene.GetActiveSceneList(scenes),
                locationPathName = $"{SERVER_PATH}/{EXE_NAME}_Server.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.EnableHeadlessMode
            };

            BuildPipeline.BuildPlayer(serverOptions);
        }

    }

}