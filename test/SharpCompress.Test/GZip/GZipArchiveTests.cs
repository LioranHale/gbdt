﻿using System;
using System.IO;
using System.Linq;
using SharpCompress.Archives;
using SharpCompress.Archives.GZip;
using Xunit;

namespace SharpCompress.Test
{
    public class GZipArchiveTests : ArchiveTests
    {
        public GZipArchiveTests()
        {
            UseExtensionInsteadOfNameToVerify = true;
        }

        [Fact]
        public void GZip_Archive_Generic()
        {
            ResetScratch();
            using (Stream stream = File.Open(Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar.gz"), FileMode.Open))
            using (var archive = ArchiveFactory.Open(stream))
            {
                var entry = archive.Entries.First();
                entry.WriteToFile(Path.Combine(SCRATCH_FILES_PATH, entry.Key));
            }
            CompareArchivesByPath(Path.Combine(SCRATCH_FILES_PATH, "Tar.tar"),
                Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar"));
        }

        [Fact]
        public void GZip_Archive()
        {
            ResetScratch();
            using (Stream stream = File.Open(Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar.gz"), FileMode.Open))
            using (var archive = GZipArchive.Open(stream))
            {
                var entry = archive.Entries.First();
                entry.WriteToFile(Path.Combine(SCRATCH_FILES_PATH, entry.Key));
            }
            CompareArchivesByPath(Path.Combine(SCRATCH_FILES_PATH, "Tar.tar"),
                Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar"));
        }


        [Fact]
        public void GZip_Archive_NoAdd()
        {
            string jpg = Path.Combine(ORIGINAL_FILES_PATH, "jpg\\test.jpg");
            ResetScratch();
            using (Stream stream = File.Open(Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar.gz"), FileMode.Open))
            using (var archive = GZipArchive.Open(stream))
            {
                Assert.Throws<InvalidOperationException>(() => archive.AddEntry("jpg\\test.jpg", jpg));
                archive.SaveTo(Path.Combine(SCRATCH_FILES_PATH, "Tar.tar.gz"));
            }
        }
    }
}
