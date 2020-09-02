using System;
using System.Threading;
using System.Threading.Tasks;

namespace K4os.Compression.LZ4.Streams
{
	/// <summary>
	/// LZ4 compression stream. 
	/// </summary>
	public partial class LZ4EncoderStream
	{
		/// <inheritdoc />
		public override void Flush() =>
			InnerFlush(EmptyToken.Value);

		/// <inheritdoc />
		public override Task FlushAsync(CancellationToken cancellationToken) =>
			InnerFlush(cancellationToken);

		/// <inheritdoc />
		public override void WriteByte(byte value)
		{
			_buffer16[_length16] = value;
			WriteImpl(EmptyToken.Value, _buffer16.AsSpan(_length16, 1));
		}

		/// <inheritdoc />
		public override void Write(byte[] buffer, int offset, int count) =>
			WriteImpl(EmptyToken.Value, buffer.AsSpan(offset, count));

		/// <inheritdoc />
		public override Task WriteAsync(
			byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
			WriteImpl(cancellationToken, buffer.AsMemory(offset, count));

		#if NETSTANDARD2_1

		/// <inheritdoc />
		public override void Write(ReadOnlySpan<byte> buffer) =>
			WriteImpl(EmptyToken.Value, buffer);

		/// <inheritdoc />
		public override ValueTask WriteAsync(
			ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) =>
			new ValueTask(WriteImpl(cancellationToken, buffer));

		#endif

		/// <inheritdoc />
		protected override void Dispose(bool disposing)
		{
			if (disposing) DisposeImpl(EmptyToken.Value);
			base.Dispose(disposing);
		}

		#if NETSTANDARD2_1

		/// <inheritdoc />
		public override async ValueTask DisposeAsync()
		{
			await DisposeImpl(CancellationToken.None);
			await base.DisposeAsync();
		}

		#endif

		/// <inheritdoc />
		public override bool CanRead => false;

		/// <summary>Length of the stream and number of bytes written so far.</summary>
		public override long Length => _position;

		/// <summary>Read-only position in the stream. Trying to set it will throw
		/// <see cref="InvalidOperationException"/>.</summary>
		public override long Position => _position;
	}
}