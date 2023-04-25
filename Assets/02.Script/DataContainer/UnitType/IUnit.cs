using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUnit
{
    public void InitContainer(Unit unitContainer);

    public void SetUnitActive(bool isActive);

    public void IsSelectedUnit(bool IsSelected);

    public void SetIsVisible(bool isVisible);
    public void SetIsLock(bool isLock);

    public void PlaySimulateUnit(bool doLoop);
    public void StopSimulateUnit();
}
