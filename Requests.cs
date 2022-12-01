namespace FluentAPIDemo;

public abstract record CalcRequest(double A, double B);
public record AddRequest(double A, double B): CalcRequest(A,B);
public record SubRequest(double A, double B): CalcRequest(A,B);
public record DivRequest(double A, double B): CalcRequest(A,B);