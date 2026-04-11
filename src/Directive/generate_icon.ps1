Add-Type -AssemblyName System.Drawing

$iconSize = 256
$fontSize = 180
$fontName = "Segoe MDL2 Assets"
$glyph = [char]0xE14E # Shopping Cart glyph

# Path to the output icon
$outputPath = Join-Path (Get-Location) "src\Directive\Resources\app_icon.ico"

# Create a bitmap
$bmp = New-Object System.Drawing.Bitmap $iconSize, $iconSize
$g = [System.Drawing.Graphics]::FromImage($bmp)
$g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
$g.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::AntiAlias
$g.Clear([System.Drawing.Color]::Transparent)

# Draw the glyph
$brush = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::RoyalBlue)
$font = New-Object System.Drawing.Font($fontName, [float]$fontSize, [System.Drawing.FontStyle]::Regular, [System.Drawing.GraphicsUnit]::Point)

$size = $g.MeasureString($glyph, $font)
$x = ($iconSize - $size.Width) / 2
$y = ($iconSize - $size.Height) / 2
$g.DrawString($glyph, $font, $brush, $x, $y)

# Save Bitmap to a MemoryStream as PNG
$ms = New-Object System.IO.MemoryStream
$bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
$pngBytes = $ms.ToArray()
$ms.Close()

# Construct ICO file (PNG-compressed)
$icoStream = [System.IO.File]::Create($outputPath)
$bw = New-Object System.IO.BinaryWriter $icoStream

# ICO Header
$bw.Write([uint16]0) # Reserved
$bw.Write([uint16]1) # Type (1 = Icon)
$bw.Write([uint16]1) # Count (1 image)

# Icon Directory Entry
$bw.Write([byte]0)   # Width (0 = 256)
$bw.Write([byte]0)   # Height (0 = 256)
$bw.Write([byte]0)   # Color Count
$bw.Write([byte]0)   # Reserved
$bw.Write([uint16]1) # Color Planes
$bw.Write([uint16]32)# Bits per pixel
$bw.Write([uint32]$pngBytes.Length) # Size of image data
$bw.Write([uint32]22) # Offset of image data (Header(6) + Entry(16))

# PNG Data
$bw.Write($pngBytes)

$bw.Close()
$icoStream.Close()

# Clean up
$g.Dispose()
$bmp.Dispose()

Write-Host "Icon generated at: $outputPath"
