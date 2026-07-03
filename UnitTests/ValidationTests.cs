using HabitTracker.matejadb;
using NUnit.Framework;

namespace UnitTests;

public class ValidationTests
{
    [Test]
    public void CorrectDateInput_ReturnsTrue()
    {
        var isValidDate = Program.ValidateDate("2026-07-03");

        Assert.That(isValidDate, Is.True);
    }

    [Test]
    public void BadDateInput_ReturnsFalse()
    {
        var isValidDate = Program.ValidateDate("202-1-3");

        Assert.That(isValidDate, Is.False);
    }
}
