﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.NET.TestFramework;
using Microsoft.NET.TestFramework.Assertions;
using Microsoft.NET.TestFramework.Commands;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.NET.Publish.Tests
{
    public class GivenThatWeWantToPublishAComServerLibrary : SdkTest
    {
        public GivenThatWeWantToPublishAComServerLibrary(ITestOutputHelper log) : base(log)
        {
        }

        [WindowsOnlyFact]
        public void It_publishes_comhost_to_the_publish_folder()
        {
            var testAsset = _testAssetsManager
                .CopyTestAsset("ComServer")
                .WithSource();

            var publishCommand = new PublishCommand(testAsset);
            publishCommand.Execute()
                .Should()
                .Pass();

            var publishDirectory = publishCommand.GetOutputDirectory(ToolsetInfo.CurrentTargetFramework);
            var outputDirectory = new BuildCommand(testAsset).GetOutputDirectory();

            var filesPublished = new[] {
                "ComServer.dll",
                "ComServer.pdb",
                "ComServer.deps.json",
                "ComServer.comhost.dll"
            };

            outputDirectory.Should().HaveFiles(filesPublished);
            publishDirectory.Should().HaveFiles(filesPublished);
        }
    }
}
