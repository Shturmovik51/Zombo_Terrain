using System;

public interface IUserInput
{
    event Action<float> OnChangeAxis;
    void GetAxis();    
}
