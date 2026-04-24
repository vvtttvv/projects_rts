using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Comments;

public record UpdateCommentRequest(
	[Required, MaxLength(2000)] string Description,
	[Required] Guid UserId,
	[Required] Guid PostId,
	Guid? ParentId
);
