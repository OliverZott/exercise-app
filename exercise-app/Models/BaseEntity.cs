﻿using SQLite;

namespace exercise_app.Models;

public class BaseEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
