﻿namespace PSK.Model.DTO
{
    public class User
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string ExpiredAt { get; set; }

    }
}
