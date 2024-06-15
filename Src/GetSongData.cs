using System;
using System.Net;

namespace BSChzzkChat.Src
{
    class GetSongData
    {
        private readonly string _baseUrl = "https://api.beatsaver.com/download/key/";
        string songCode = "";

        public GetSongData(string songCode)
        {
            this.songCode = songCode;
        }

        // 다운로드 할 파일 데이터 받아오기
        public string GetFileData()
        {
            WebClient webClient = new WebClient();
            Uri downloadUri = new Uri(String.Format($"{_baseUrl}{songCode}"));
            string fileName = "";

            try
            {
                // 파일 데이터 받기
                var data = webClient.DownloadData(downloadUri);
                // 헤더에서 원하는 내용 있는지 확인
                string header = webClient.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                // 파일 이름부분 검색용 
                const string filename = "filename=";
                // 파일 이름 검색용 끝나는 위치 확인
                int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
                // 파일 이름 가져오기
                if (index > -1)
                {
                    fileName = header.Substring(index + filename.Length);
                    fileName = fileName.Substring(1, fileName.Length - 2);
                }

                return fileName;
            }
            catch
            {
                return fileName;
            }
        }
    }
}
