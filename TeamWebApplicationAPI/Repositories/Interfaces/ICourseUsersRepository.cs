﻿using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Repositories.Interfaces
{
    public interface ICourseUsersRepository
    {
        Task<IEnumerable<int>> GetUsersByCourseIdAsync(int? courseId);
        Task<IEnumerable<int>> GetCoursesByUserIdAsync(int? userId);
        Task<IEnumerable<CourseUser>> GetRelationsByUserIdAsync(int? userId);
        Task<bool> CheckIfRelationExistsAsync(int? courseId, int? userId);
        Task InsertRelationAsync(int? courseId, int? userId);
        Task DeleteRelationAsync(int? courseId, int? userId);
        Task SaveAsync();
    }
}
