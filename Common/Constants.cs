namespace Common
{
    public static class Constants
    {
        public static string ConnectionString = "Data Source = CartoonsRepo.sdf;Persist Security Info=False";

        public static string[] DatabaseDef =
            {
                @"CREATE TABLE FileDetail (
                    Id int IDENTITY (1,1) NOT NULL,
                    ParentId INT NULL,
                    FileName  nvarchar(255) NULL,
                    Year nvarchar(20),--
                    Format nvarchar(25) NULL,
                    Encoded_Application nvarchar(255) NULL,
                    FileSize nvarchar(25) NULL,
                    FileSize2 nvarchar(25) NULL,
                    Duration datetime NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    CoverEmbedded nvarchar(5) NULL,
                    Season nvarchar(20) NULL,--
                    Theme nvarchar(255) NULL,--
                    StreamLink nvarchar(255) NULL,
                    InsertedDate datetime NULL,
                    LastChangeDate datetime NULL,
                    Quality nvarchar(25),           --friendly named set at import based on the first video stream height
                    Recommended nvarchar(5) NULL,--
                    RecommendedLink nvarchar(255) NULL,--
                    DescriptionLink nvarchar(255) NULL,--
                    Notes nvarchar(255) NULL,--
                    Poster image NULL,  -- for movies and series!!!
                    AudioLanguages nvarchar(255),
                    SubtitleLanguages nvarchar(255))",

                @"ALTER TABLE FileDetail ADD CONSTRAINT PK_FileDetail PRIMARY KEY (Id)",


                @"CREATE TABLE Thumbnails (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    MovieStill image NULL)",

                @"ALTER TABLE Thumbnails ADD CONSTRAINT PK_Thumbnails PRIMARY KEY (Id)",

                @"ALTER TABLE Thumbnails ADD CONSTRAINT FK_FileDetail_Thumbnails
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)",


                @"CREATE TABLE VideoStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    Format_Profile nvarchar(50) NULL,
                    BitRateMode nvarchar(50) NULL,
                    BitRate nvarchar(50) NULL,
                    Width nvarchar(10) NULL,
                    Height nvarchar(10) NULL,
                    FrameRate_Mode nvarchar(50) NULL,
                    FrameRate nvarchar(50) NULL,
                    Delay nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE VideoStream ADD CONSTRAINT PK_VideoStream PRIMARY KEY (Id)",

                @"ALTER TABLE VideoStream ADD CONSTRAINT FK_FileDetail_VideoStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)",


                @"CREATE TABLE AudioStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    BitRate nvarchar(50) NULL,
                    Channel nvarchar(50) NULL,
                    ChannelPosition nvarchar(50) NULL,
                    SamplingRate nvarchar(50) NULL,
                    Resolution nvarchar(50) NULL,
                    Delay nvarchar(50) NULL,
                    Video_Delay nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE AudioStream ADD CONSTRAINT PK_AudioStream PRIMARY KEY (Id)",

                @"ALTER TABLE AudioStream ADD CONSTRAINT FK_FileDetail_AudioStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)",


                @"CREATE TABLE SubtitleStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE SubtitleStream ADD CONSTRAINT PK_SubtitleStream PRIMARY KEY (Id)",

                @"ALTER TABLE SubtitleStream ADD CONSTRAINT FK_FileDetail_SubtitleStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)"
            };

        /*
        public static string[] DatabaseDef =
            {
                @"CREATE TABLE MovieOrSeries (
                    Id int IDENTITY (1,1) NOT NULL,
                    Recommended nvarchar(5) NULL,
                    RecommendedLink nvarchar(255) NULL,
                    DescriptionLink nvarchar(255) NULL,
                    Poster image NULL,
                    IsSerie bit NOT NULL DEFAULT 0,
                    Notes nvarchar(255) NULL)",

                @"ALTER TABLE MovieOrSeries ADD CONSTRAINT PK_MovieOrSeries PRIMARY KEY (Id)",

                @"CREATE TABLE FileDetail (
                    Id int IDENTITY (1,1) NOT NULL,
                    MovieOrSeriesId INT NULL,
                    ParentId INT NULL,
                    FileName  nvarchar(255) NULL,
                    Year nvarchar(20),
                    Format nvarchar(25) NULL,
                    Encoded_Application nvarchar(255) NULL,
                    FileSize nvarchar(25) NULL,
                    FileSize2 nvarchar(25) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    CoverEmbedded nvarchar(5) NULL,
                    Season nvarchar(20) NULL,
                    Theme nvarchar(255) NULL,
                    StreamLink nvarchar(255) NULL,
                    InsertedDate datetime NULL,
                    LastChangeDate datetime NULL,
                    Quality nvarchar(25),           //friendly named set at import based on the first video stream height
                    HasPoster bit NOT NULL DEFAULT 0)",

                @"ALTER TABLE FileDetail ADD CONSTRAINT PK_FileDetail PRIMARY KEY (Id)",

                @"ALTER TABLE FileDetail ADD CONSTRAINT FK_MovieOrSeries_FileDetail
                  FOREIGN KEY (MovieOrSeriesId) REFERENCES MovieOrSeries(Id)",

                @"CREATE TABLE VideoStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    Format_Profile nvarchar(50) NULL,
                    BitRateMode nvarchar(50) NULL,
                    BitRate nvarchar(50) NULL,
                    Width nvarchar(10) NULL,
                    Height nvarchar(10) NULL,
                    FrameRate_Mode nvarchar(50) NULL,
                    FrameRate nvarchar(50) NULL,
                    Delay nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE VideoStream ADD CONSTRAINT PK_VideoStream PRIMARY KEY (Id)",

                @"ALTER TABLE VideoStream ADD CONSTRAINT FK_FileDetail_VideoStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)",

                @"CREATE TABLE AudioStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    Channel nvarchar(50) NULL,
                    ChannelPosition nvarchar(50) NULL,
                    SamplingRate nvarchar(50) NULL,
                    Resolution nvarchar(50) NULL,
                    Delay nvarchar(50) NULL,
                    Video_Delay nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE AudioStream ADD CONSTRAINT PK_AudioStream PRIMARY KEY (Id)",

                @"ALTER TABLE AudioStream ADD CONSTRAINT FK_FileDetail_AudioStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)",

                @"CREATE TABLE SubtitleStream (
                    Id int IDENTITY (1,1) NOT NULL,
                    FileDetailId int NOT NULL,
                    [Index] int NOT NULL,
                    Format nvarchar(50) NULL,
                    StreamSize nvarchar(50) NULL,
                    TitleEmbedded nvarchar(255) NULL,
                    Language nvarchar(50) NULL)",

                @"ALTER TABLE SubtitleStream ADD CONSTRAINT PK_SubtitleStream PRIMARY KEY (Id)",

                @"ALTER TABLE SubtitleStream ADD CONSTRAINT FK_FileDetail_SubtitleStream
                  FOREIGN KEY (FileDetailId) REFERENCES FileDetail(Id)"
            };
            */
    }
}
