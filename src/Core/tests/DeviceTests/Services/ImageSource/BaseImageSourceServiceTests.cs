using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.ImageSource)]
	public abstract partial class BaseImageSourceServiceTests : TestBase
	{
	}

	public static class ImageSourceServiceExtensions
	{
#if __ANDROID__
		public static Task<IImageSourceServiceResult<Android.Graphics.Drawables.Drawable>> GetImageAsync(
			this IImageSourceService service,
			IImageSource imageSource,
			CancellationToken cancellationToken = default) =>
			service.GetDrawableAsync(imageSource, MauiProgram.DefaultContext, cancellationToken);
#elif WINDOWS
		public static Task<IImageSourceServiceResult<UI.Xaml.Media.ImageSource>> GetImageAsync(
			this IImageSourceService service,
			IImageSource imageSource,
			CancellationToken cancellationToken = default) =>
			service.GetImageSourceAsync(imageSource, cancellationToken: cancellationToken);
#endif
	}
}