using System.Collections.Generic;
using UnityEditor;

public class CsprojPostprocessor : AssetPostprocessor
{


    private static Dictionary<string, string> ModBundleMap = new Dictionary<string, string>(){
        {"CatsCards","assetbundle"} ///EDIT WITH NAME OF MOD ASSEMBLY AND NAME OF ASSET BUNDLE (CASE MATTERS)
    };

    // public static string OnGeneratedCSProject(string path, string content)
    // {
    //     foreach (var mod in ModBundleMap.Keys)
    //     {
    //         if (!path.EndsWith($"{mod}.csproj"))
    //         {
    //             continue;
    //         }
    //         string newContent = "";
    //         bool Added = false;
    //         bool isPropGroup = false;
    //         var lines = content.Split('\n');
    //         for (int i = 0; i < lines.Length; i++)
    //         {
    //             string line = lines[i];
    //             if (line.Contains("<LangVersion>latest</LangVersion>"))
    //             {
    //                 line.Replace("<LangVersion>latest</LangVersion>", "<LangVersion>preview</LangVersion>");
    //             }
    //             if (!Added && line.Contains("<Compile Include="))
    //             {
    //                 newContent += $"     <EmbeddedResource Include=\"Assets\\AssetBundles\\{ModBundleMap[mod]}\" />\n";
    //                 Added = true;
    //             }
    //             if (isPropGroup)
    //             {
    //                 if (line.Contains("</PropertyGroup>"))
    //                 {
    //                     isPropGroup = false;
    //                     newContent += $"{line}\n";
    //                 }
    //                 continue;
    //             }
    //             newContent += $"{line}\n";

    //             if (line.Contains("<PropertyGroup Condition=\"'$(Configuration)|$(Platform)'=='Debug|AnyCPU'\">"))
    //             {
    //                 isPropGroup = true;
    //                 string s = @"      <DebugSymbols>true</DebugSymbols>
    //<DebugType>embedded</DebugType>
    //<PathMap>$([System.IO.Path]::GetFullPath('$(MSBuildThisFileDirectory)'))=./</PathMap>
    //<Optimize>false</Optimize>
    //<DefineConstants>DEBUG</DefineConstants>
    //<AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    //   <OutputPath>Temp\bin\Debug\</OutputPath>
    //   <DefineConstants>DEBUG;TRACE;UNITY_5_3_OR_NEWER;UNITY_5_4_OR_NEWER;UNITY_5_5_OR_NEWER;UNITY_5_6_OR_NEWER;UNITY_2017_1_OR_NEWER;UNITY_2017_2_OR_NEWER;UNITY_2017_3_OR_NEWER;UNITY_2017_4_OR_NEWER;UNITY_2018_1_OR_NEWER;UNITY_2018_2_OR_NEWER;UNITY_2018_3_OR_NEWER;UNITY_2018_4_OR_NEWER;UNITY_2018_4_34;UNITY_2018_4;UNITY_2018;PLATFORM_ARCH_64;UNITY_64;UNITY_INCLUDE_TESTS;ENABLE_AUDIO;ENABLE_CACHING;ENABLE_CLOTH;ENABLE_DUCK_TYPING;ENABLE_MICROPHONE;ENABLE_MULTIPLE_DISPLAYS;ENABLE_PHYSICS;ENABLE_SPRITES;ENABLE_GRID;ENABLE_TILEMAP;ENABLE_TERRAIN;ENABLE_TEXTURE_STREAMING;ENABLE_DIRECTOR;ENABLE_UNET;ENABLE_LZMA;ENABLE_UNITYEVENTS;ENABLE_WEBCAM;ENABLE_WWW;ENABLE_CLOUD_SERVICES_COLLAB;ENABLE_CLOUD_SERVICES_COLLAB_SOFTLOCKS;ENABLE_CLOUD_SERVICES_ADS;ENABLE_CLOUD_HUB;ENABLE_CLOUD_PROJECT_ID;ENABLE_CLOUD_SERVICES_USE_WEBREQUEST;ENABLE_CLOUD_SERVICES_UNET;ENABLE_CLOUD_SERVICES_BUILD;ENABLE_CLOUD_LICENSE;ENABLE_EDITOR_HUB;ENABLE_EDITOR_HUB_LICENSE;ENABLE_WEBSOCKET_CLIENT;ENABLE_DIRECTOR_AUDIO;ENABLE_DIRECTOR_TEXTURE;ENABLE_TIMELINE;ENABLE_EDITOR_METRICS;ENABLE_EDITOR_METRICS_CACHING;ENABLE_MANAGED_JOBS;ENABLE_MANAGED_TRANSFORM_JOBS;ENABLE_MANAGED_ANIMATION_JOBS;INCLUDE_DYNAMIC_GI;INCLUDE_GI;ENABLE_MONO_BDWGC;PLATFORM_SUPPORTS_MONO;RENDER_SOFTWARE_CURSOR;INCLUDE_PUBNUB;ENABLE_VIDEO;ENABLE_CUSTOM_RENDER_TEXTURE;ENABLE_LOCALIZATION;PLATFORM_STANDALONE_WIN;PLATFORM_STANDALONE;UNITY_STANDALONE_WIN;UNITY_STANDALONE;ENABLE_SUBSTANCE;ENABLE_RUNTIME_GI;ENABLE_MOVIES;ENABLE_NETWORK;ENABLE_CRUNCH_TEXTURE_COMPRESSION;ENABLE_UNITYWEBREQUEST;ENABLE_CLOUD_SERVICES;ENABLE_CLOUD_SERVICES_ANALYTICS;ENABLE_CLOUD_SERVICES_PURCHASING;ENABLE_CLOUD_SERVICES_CRASH_REPORTING;ENABLE_OUT_OF_PROCESS_CRASH_HANDLER;ENABLE_EVENT_QUEUE;ENABLE_CLUSTER_SYNC;ENABLE_CLUSTERINPUT;ENABLE_VR;ENABLE_AR;ENABLE_WEBSOCKET_HOST;ENABLE_MONO;NET_4_6;ENABLE_PROFILER;UNITY_ASSERTIONS;UNITY_EDITOR;UNITY_EDITOR_64;UNITY_EDITOR_WIN;ENABLE_UNITY_COLLECTIONS_CHECKS;ENABLE_BURST_AOT;UNITY_TEAM_LICENSE;CSHARP_7_OR_LATER;CSHARP_7_3_OR_NEWER</DefineConstants>
    //   <ErrorReport>prompt</ErrorReport>
    //   <WarningLevel>4</WarningLevel>
    //   <NoWarn>0169</NoWarn>";
    //                 newContent += $"{s}\n";
    //             }
    //         }
    //         return newContent;
    //     }
    //     return content;
    // }
    public static string OnGeneratedCSProject(string path, string content)
    {
        foreach (var mod in ModBundleMap.Keys)
        {
            if (!path.EndsWith($"{mod}.csproj"))
            {
                continue;
            }
            string newContent = "";
            bool Added = false;
            var lines = content.Split('\n');
            foreach (var line in lines)
            {
                if (!Added && line.Contains("<Compile Include="))
                {
                    newContent += $"     <EmbeddedResource Include=\"Assets\\AssetBundles\\{ModBundleMap[mod]}\" />\n";
                    Added = true;
                }
                newContent += $"{line}\n";
            }
            return newContent;
        }
        return content;
    }
}