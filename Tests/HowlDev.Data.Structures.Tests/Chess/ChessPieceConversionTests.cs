using HowlDev.Data.Structures.Games.Chess;

namespace HowlDev.Data.Structures.Tests.Chess;

// AI generated suite for my static class
public class ChessPieceConversionGetPieceTests {
    [Test]
    [Arguments(0x09, ChessPiece.King)]  // White King
    [Arguments(0x0a, ChessPiece.Queen)]  // White Queen
    [Arguments(0x0b, ChessPiece.Bishop)]  // White Bishop
    [Arguments(0x0c, ChessPiece.Knight)]  // White Knight
    [Arguments(0x0d, ChessPiece.Rook)]  // White Rook
    [Arguments(0x0e, ChessPiece.Pawn)]  // White Pawn
    public async Task GetPieceReturnsWhitePiece(byte piece, ChessPiece expectedPiece) {
        var result = ChessPieceConversion.GetPiece(piece);
        
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Piece).IsEqualTo(expectedPiece);
        await Assert.That(result!.Value.White).IsTrue();
    }

    [Test]
    [Arguments(0x01, ChessPiece.King)]  // Black King
    [Arguments(0x02, ChessPiece.Queen)]  // Black Queen
    [Arguments(0x03, ChessPiece.Bishop)]  // Black Bishop
    [Arguments(0x04, ChessPiece.Knight)]  // Black Knight
    [Arguments(0x05, ChessPiece.Rook)]  // Black Rook
    [Arguments(0x06, ChessPiece.Pawn)]  // Black Pawn
    public async Task GetPieceReturnsBlackPiece(byte piece, ChessPiece expectedPiece) {
        var result = ChessPieceConversion.GetPiece(piece);
        
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value.Piece).IsEqualTo(expectedPiece);
        await Assert.That(result!.Value.White).IsFalse();
    }

    [Test]
    [Arguments(0)]  // Invalid: 0 & 0x07 = 0
    [Arguments(7)]  // Invalid: 7 & 0x07 = 7
    [Arguments(15)]  // Invalid: 15 & 0x07 = 7
    [Arguments(0x0F)]  // Invalid: 0x0F & 0x07 = 7
    [Arguments(0xFF)]  // Invalid: 0xFF & 0x07 = 7
    public async Task GetPieceReturnsNullForInvalidPiece(byte piece) {
        var result = ChessPieceConversion.GetPiece(piece);
        
        await Assert.That(result).IsNull();
    }
}

public class ChessPieceConversionGetPieceRepresentationTests {
    [Test]
    [Arguments(1, 'k')]  // Black King
    [Arguments(0x09, 'K')]  // White King
    [Arguments(2, 'q')]  // Black Queen
    [Arguments(0x0A, 'Q')]  // White Queen
    [Arguments(3, 'b')]  // Black Bishop
    [Arguments(0x0B, 'B')]  // White Bishop
    [Arguments(4, 'n')]  // Black Knight
    [Arguments(0x0C, 'N')]  // White Knight
    [Arguments(5, 'r')]  // Black Rook
    [Arguments(0x0D, 'R')]  // White Rook
    [Arguments(6, 'p')]  // Black Pawn
    [Arguments(0x0E, 'P')]  // White Pawn
    public async Task GetPieceRepresentationReturnsCorrectChar(byte piece, char expectedChar) {
        var result = ChessPieceConversion.GetPieceRepresentation(piece);
        
        await Assert.That(result).IsNotNull();
        await Assert.That(result!.Value).IsEqualTo(expectedChar);
    }

    [Test]
    [Arguments(0)]  // Invalid
    [Arguments(7)]  // Invalid
    [Arguments(15)]  // Invalid
    [Arguments(0x0F)]  // Invalid
    [Arguments(0xFF)]  // Invalid
    public async Task GetPieceRepresentationReturnsNullForInvalidPiece(byte piece) {
        var result = ChessPieceConversion.GetPieceRepresentation(piece);
        
        await Assert.That(result).IsNull();
    }
}

public class ChessPieceConversionGetByteTests {
    [Test]
    [Arguments(ChessPiece.King, true, 0x09)]
    [Arguments(ChessPiece.Queen, true, 0x0A)]
    [Arguments(ChessPiece.Bishop, true, 0x0B)]
    [Arguments(ChessPiece.Knight, true, 0x0C)]
    [Arguments(ChessPiece.Rook, true, 0x0D)]
    [Arguments(ChessPiece.Pawn, true, 0x0E)]
    public async Task GetByteReturnsCorrectBytForWhitePiece(ChessPiece piece, bool white, byte expected) {
        var result = ChessPieceConversion.GetByte(piece, white);
        
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(ChessPiece.King, false, 0x01)]
    [Arguments(ChessPiece.Queen, false, 0x02)]
    [Arguments(ChessPiece.Bishop, false, 0x03)]
    [Arguments(ChessPiece.Knight, false, 0x04)]
    [Arguments(ChessPiece.Rook, false, 0x05)]
    [Arguments(ChessPiece.Pawn, false, 0x06)]
    public async Task GetByteReturnsCorrectByteForBlackPiece(ChessPiece piece, bool white, byte expected) {
        var result = ChessPieceConversion.GetByte(piece, white);
        
        await Assert.That(result).IsEqualTo(expected);
    }
}

public class ChessPieceConversionRoundTripTests {
    [Test]
    [Arguments(1, ChessPiece.King)]  // Black King
    [Arguments(0x09, ChessPiece.King)]  // White King
    [Arguments(2, ChessPiece.Queen)]  // Black Queen
    [Arguments(0x0A, ChessPiece.Queen)]  // White Queen
    [Arguments(3, ChessPiece.Bishop)]  // Black Bishop
    [Arguments(0x0B, ChessPiece.Bishop)]  // White Bishop
    [Arguments(4, ChessPiece.Knight)]  // Black Knight
    [Arguments(0x0C, ChessPiece.Knight)]  // White Knight
    [Arguments(5, ChessPiece.Rook)]  // Black Rook
    [Arguments(0x0D, ChessPiece.Rook)]  // White Rook
    [Arguments(6, ChessPiece.Pawn)]  // Black Pawn
    [Arguments(0x0E, ChessPiece.Pawn)]  // White Pawn
    public async Task ByteToPieceToBytRoundTrip(byte originalByte, ChessPiece expectedPiece) {
        var pieceData = ChessPieceConversion.GetPiece(originalByte);
        
        await Assert.That(pieceData).IsNotNull();
        await Assert.That(pieceData!.Value.Piece).IsEqualTo(expectedPiece);
        
        byte roundTripByte = ChessPieceConversion.GetByte(pieceData!.Value.Piece, pieceData!.Value.White);
        
        await Assert.That(roundTripByte).IsEqualTo(originalByte);
    }

    [Test]
    [Arguments(ChessPiece.King, true)]
    [Arguments(ChessPiece.King, false)]
    [Arguments(ChessPiece.Queen, true)]
    [Arguments(ChessPiece.Queen, false)]
    [Arguments(ChessPiece.Bishop, true)]
    [Arguments(ChessPiece.Bishop, false)]
    [Arguments(ChessPiece.Knight, true)]
    [Arguments(ChessPiece.Knight, false)]
    [Arguments(ChessPiece.Rook, true)]
    [Arguments(ChessPiece.Rook, false)]
    [Arguments(ChessPiece.Pawn, true)]
    [Arguments(ChessPiece.Pawn, false)]
    public async Task PieceToByteToCharRoundTrip(ChessPiece piece, bool white) {
        byte pieceData = ChessPieceConversion.GetByte(piece, white);
        
        char? representation = ChessPieceConversion.GetPieceRepresentation(pieceData);
        
        await Assert.That(representation).IsNotNull();
        
        bool isWhiteExpected = white;
        bool isUpperCase = char.IsUpper(representation!.Value);
        
        await Assert.That(isUpperCase).IsEqualTo(isWhiteExpected);
    }

    [Test]
    [MethodDataSource(nameof(AllValidPiecesSource))]
    public async Task AllPiecesRoundTripThroughAllMethods(ChessPiece piece, bool white) {
        // Piece -> Byte
        byte byteValue = ChessPieceConversion.GetByte(piece, white);
        
        // Byte -> Piece
        var pieceData = ChessPieceConversion.GetPiece(byteValue);
        await Assert.That(pieceData).IsNotNull();
        await Assert.That(pieceData!.Value.Piece).IsEqualTo(piece);
        await Assert.That(pieceData!.Value.White).IsEqualTo(white);
        
        // Byte -> Char
        char? charValue = ChessPieceConversion.GetPieceRepresentation(byteValue);
        await Assert.That(charValue).IsNotNull();
        await Assert.That(char.IsUpper(charValue!.Value)).IsEqualTo(white);
    }

    public static IEnumerable<Func<(ChessPiece, bool)>> AllValidPiecesSource() {
        ChessPiece[] pieces = [ChessPiece.King, ChessPiece.Queen, ChessPiece.Bishop, ChessPiece.Knight, ChessPiece.Rook, ChessPiece.Pawn];
        bool[] colors = [true, false];
        
        foreach (var piece in pieces) {
            foreach (var color in colors) {
                yield return () => (piece, color);
            }
        }
    }
}
